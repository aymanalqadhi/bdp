using BDP.Application.App.Exceptions;
using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;

using Isopoh.Cryptography.Argon2;

using System.Linq.Expressions;

namespace BDP.Application.App;

public class AuthService : IAuthService
{
    private readonly ILegacyUnitOfWork _uow;
    private readonly AuthSettings _settings = new();
    private readonly IRandomGeneratorService _rngSvc;
    private readonly IPasswordHashingService _passwordHashingSvc;
    private readonly IEmailService _emailSvc;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    /// <param name="configSvc">The service used to manage configuration</param>
    /// <param name="rngSvc">The service used to generate random values</param>
    /// <param name="emailSvc">The service used to send emails</param>
    public AuthService(
        ILegacyUnitOfWork uow,
        IConfigurationService configSvc,
        IRandomGeneratorService rngSvc,
        IPasswordHashingService passwordHashingSvc,
        IEmailService emailSvc)
    {
        configSvc.Bind(nameof(AuthSettings), _settings);

        _uow = uow;
        _rngSvc = rngSvc;
        _passwordHashingSvc = passwordHashingSvc;
        _emailSvc = emailSvc;
    }

    /// <inheritdoc/>
    public async Task<(User, RefreshToken)> SignInAsync(
        string username,
        string password,
        Func<string> tokenGenerator,
        LoginDeviceInfo deviceInfo)

    {
        var user = await _uow.Users.FirstOrDefaultAsync(
            u => u.Username == username,
            includes: new Expression<Func<User, object>>[] { u => u.Groups });

        if (user == null || !_passwordHashingSvc.Verify(password, user.PasswordHash))
            throw new InvalidUsernameOrPasswordException();

        if (await _uow.RefreshTokens.FirstOrDefaultAsync(
            r => r.Owner.Id == user.Id &&
            r.UniqueIdentifier == deviceInfo.UniqueIdentifier &&
            r.ValidUntil > DateTime.Now)
            is var token && token is not null)
        {
            token.LastLogin = DateTime.Now;

            _uow.RefreshTokens.Update(token);
            await _uow.CommitAsync();

            return (user, token);
        }

        if (await _uow.RefreshTokens.CountAsync(
                r => r.Owner.Id == user.Id && r.ValidUntil > DateTime.Now) >= _settings.MaxSessionsCount)
        {
            throw new MaxSessionsCountExceeded(_settings.MaxSessionsCount);
        }

        var refreshToken = new RefreshToken
        {
            Owner = user,
            Token = tokenGenerator(),

            UniqueIdentifier = deviceInfo.UniqueIdentifier,
            LastIpAddress = deviceInfo.LastIpAddress,
            LastLogin = DateTime.Now,
            HostName = deviceInfo.HostName,
            DeviceName = deviceInfo.DeviceName,

            ValidUntil = DateTime.Now.Add(_settings.RefreshTokenValidity),
        };

        _uow.RefreshTokens.Add(refreshToken);
        await _uow.CommitAsync();

        return (user, refreshToken);
    }

    /// <inheritdoc/>
    public Task<bool> IsTokenValidAsync(User user, string token, string uniqueId)
        => _uow.RefreshTokens.AnyAsync(
            r => r.Owner.Id == user.Id &&
            r.Token == token &&
            r.UniqueIdentifier == uniqueId &&
            r.ValidUntil > DateTime.Now);

    /// <inheritdoc/>
    public async Task InvalidateTokenAsync(User user, string token, string uniqueId)
    {
        var refreshToken = await _uow.RefreshTokens.FirstOrDefaultAsync(r =>
           r.Owner.Id == user.Id &&
           r.Token == token &&
           r.UniqueIdentifier == uniqueId);

        if (refreshToken is null)
            throw new NotFoundException("no such token");

        _uow.RefreshTokens.Remove(refreshToken);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<User> SignUpAsync(string username, string email, string password)
    {
        if (await _uow.Users.AnyAsync(u => u.Username == username))
            throw new AlreadyUsedUsernameException(username);

        if (await _uow.Users.AnyAsync(u => u.Email == email))
            throw new AlreadyUsedEmailException(email);

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = _passwordHashingSvc.Hash(password),
            IsActive = false,
            IsConfirmed = false,
        };

        _uow.Users.Add(user);

        await _uow.CommitAsync();
        await SendConfirmationMessage(user, "Confirm your account");

        return user;
    }

    /// <inheritdoc/>
    public async Task<Confirmation> SendConfirmationMessage(User user, string title)
    {
        var confirmation = new Confirmation
        {
            OneTimePassword = _rngSvc.RandomString(_settings.ConfirmationOtpLength, RandomStringKind.AlphaNum),
            Token = _rngSvc.RandomString(_settings.ConfirmationTokenLength, RandomStringKind.AlphaNum),
            ForUser = user,
            ValidFor = _settings.ConfirmationValidity,
        };

        _uow.Confirmations.Add(confirmation);
        await _uow.CommitAsync();

        await _emailSvc.SendHtmlEmail(
            user.Email,
            title,
            BuildConfirmationHtmlEmail(confirmation));

        return confirmation;
    }

    /// <inheritdoc/>
    public async Task ConfirmWithOtp(string otp)
        => await DoActivateAccount(
            await _uow.Confirmations.FirstOrDefaultAsync(c => c.OneTimePassword == otp,
                includes: new Expression<Func<Confirmation, object>>[] { c => c.ForUser }));

    /// <inheritdoc/>
    public async Task ConfirmWithToken(string token)
    => await DoActivateAccount(
        await _uow.Confirmations.FirstOrDefaultAsync(c => c.Token == token,
            includes: new Expression<Func<Confirmation, object>>[] { c => c.ForUser }));

    private async Task DoActivateAccount(Confirmation? confirmation)
    {
        if (confirmation == null)
            throw new NotFoundException($"invalid confirmation code");

        confirmation.ForUser.IsConfirmed = true;
        confirmation.ForUser.IsActive = true;

        _uow.Users.Update(confirmation.ForUser);
        _uow.Confirmations.Remove(confirmation);

        await _uow.CommitAsync();
    }

    private string BuildConfirmationHtmlEmail(Confirmation c)
    {
        return @$"
<p>
    Hello, <b>{c.ForUser.Username}</b>!
</p>
<p style= ""line-height: 2em"">
    Your activation code is:
    <b style=""color:#414141; background-color: #efefef; padding: 8px 16px"">
        {c.OneTimePassword}
    </b>
    <br>
    Activate directly
    <a href = ""{_settings.TokenActivationBaseUrl}/{c.Token}"">
        from here
    </a>
    <br>
</p>
<footer> This email expires in <b>{c.ValidFor}.</footer>
";
    }
}

internal class AuthSettings
{
    /// <summary>
    /// Gets or sets the maximum sessions count per user
    /// </summary>
    public int MaxSessionsCount { get; set; } = 10;

    /// <summary>
    /// Gets or sets the base url of the activation emails
    /// </summary>
    public string TokenActivationBaseUrl { get; set; } = null!;

    /// <summary>
    /// Gets or sets the length of the confirmation otp
    /// </summary>
    public int ConfirmationOtpLength { get; set; } = 6;

    /// <summary>
    /// Gets or sets the length of the confirmation token
    /// </summary>
    public int ConfirmationTokenLength { get; set; } = 64;

    /// <summary>
    /// Gets or sets the confirmation validity period
    /// </summary>
    public TimeSpan ConfirmationValidity { get; set; } = TimeSpan.FromMinutes(30);

    /// <summary>
    /// Gets or sets the referesh token validity
    /// </summary>
    public TimeSpan RefreshTokenValidity { get; set; } = TimeSpan.FromDays(120);
}
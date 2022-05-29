using BDP.Domain.Entities;

namespace BDP.Domain.Services.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Asynchronously logs a user in to an account
    /// </summary>
    /// <param name="username">The username of the user</param>
    /// <param name="password">The password of the user</param>
    /// <param name="tokenGenerator">The function used to generate tokens</param>
    /// <param name="deviceInfo">The info of the device that requested the token</param>
    /// <returns>The created refresh token</returns>
    Task<(User, RefreshToken)> SignInAsync(
        string username,
        string password,
        Func<string> tokenGenerator,
        LoginDeviceInfo deviceInfo);

    /// <summary>
    /// Asynchronously checks if a token is valid or not
    /// </summary>
    /// <param name="user">The owner of the token</param>
    /// <param name="token">The token to check</param>
    /// <param name="uniqueId">The device unique id</param>
    /// <returns></returns>
    Task<bool> IsTokenValidAsync(User user, string token, string uniqueId);

    /// <summary>
    /// Asynchronously invlidates a refresh token
    /// </summary>
    /// <param name="user">The owner of the token</param>
    /// <param name="token">The token to check</param>
    /// <param name="uniqueId">The device unique id</param>
    /// <returns></returns>
    Task InvalidateTokenAsync(User user, string token, string uniqueId);

    /// <summary>
    /// Asynchronously registers a user
    /// </summary>
    /// <param name="username">The username of the user</param>
    /// <param name="email">The email of the user</param>
    /// <param name="password">The password of the user</param>
    /// <returns>The created user account</returns>
    Task<User> SignUpAsync(string username, string email, string password);

    /// <summary>
    /// Asynchronously sends a confirmation message to the user
    /// </summary>
    /// <param name="user">The user to which to send the message</param>
    /// <param name="title">The title of the message</param>
    /// <returns>The sent message</returns>
    Task<Confirmation> SendConfirmationMessage(User user, string title);

    /// <summary>
    /// Asynchrnously confirms a user account with otp
    /// </summary>
    /// <param name="otp">The otp to use</param>
    /// <returns></returns>
    Task ConfirmWithOtp(string otp);

    /// <summary>
    /// Asynchrnously confirms a user account with token
    /// </summary>
    /// <param name="token">The token to use</param>
    /// <returns></returns>
    Task ConfirmWithToken(string token);
}

public class LoginDeviceInfo
{
    /// <summary>
    /// Gets or sets the device unique identifier
    /// </summary>
    public string UniqueIdentifier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last ip address that used the token
    /// </summary>
    public string LastIpAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the device name of the device that uses the token
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    /// Gets or sets the hostname of the device that uses the token
    /// </summary>
    public string? HostName { get; set; }
}
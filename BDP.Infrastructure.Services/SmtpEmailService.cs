using BDP.Domain.Services;

using System.Linq;
using System.Net;
using System.Net.Mail;

namespace BDP.Infrastructure.Services;

public class SmtpEmailService : IEmailService
{
    #region Fields

    private readonly SmtpSettings _settings = new();

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="configSvc">configuration service</param>
    public SmtpEmailService(IConfigurationService configSvc)
        => configSvc.Bind(nameof(SmtpSettings), _settings);

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task SendEmail(
        string to,
        string subject,
        string body,
        EmailOption options,
        IEnumerable<FileStream>? attachments = null)
    {
        using var smtp = new SmtpClient(_settings.Host, _settings.Port);
        smtp.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
        smtp.EnableSsl = _settings.EnableSSL;

        using var msg = new MailMessage(_settings.Username, to, subject, body);
        msg.Sender = new MailAddress(_settings.Username);
        msg.IsBodyHtml = options.HasFlag(EmailOption.HasHtmlBody);

        if (attachments != null)
        {
            foreach (var a in attachments)
                msg.Attachments.Add(new Attachment(a, a.Name));
        }

        try
        {
            await smtp.SendMailAsync(msg);
        }
        catch (SmtpFailedRecipientException ex)
        {
            if (ex.StatusCode == SmtpStatusCode.MailboxBusy ||
                ex.StatusCode == SmtpStatusCode.MailboxUnavailable ||
                ex.StatusCode == SmtpStatusCode.TransactionFailed)
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                await smtp.SendMailAsync(msg);
            }
            else
            {
                throw;
            }
        }
    }

    #endregion Public Methods
}

internal class SmtpSettings
{
    #region Properties

    /// <summary>
    /// Gets or sets whether the smtp server uses ssl/tls
    /// </summary>
    public bool EnableSSL { get; set; } = true;

    /// <summary>
    /// Gets or sets the smtp host name
    /// </summary>
    public string Host { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password used to login to the smtp server
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Gets or sets the smtp server port
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the username used to login to the smtp server
    /// </summary>
    public string Username { get; set; } = null!;

    #endregion Properties
}
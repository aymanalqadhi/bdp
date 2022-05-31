using BDP.Domain.Services;
using System.Net;
using System.Net.Mail;

namespace BDP.Infrastructure.Services;

public class SmtpEmailService : IEmailService
{
    private readonly SmtpSettings _settings = new();

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="configSvc">configuration service</param>
    public SmtpEmailService(IConfigurationService configSvc)
        => configSvc.Bind(nameof(SmtpSettings), _settings);

    /// <inheritdoc/>
    public Task SendEmail(string to, string subject, string body, IList<FileStream>? attachments = null)
        => DoSendEmail(to, subject, body, false, attachments);

    /// <inheritdoc/>
    public Task SendHtmlEmail(string to, string subject, string htmlBody, IList<FileStream>? attachments = null)
        => DoSendEmail(to, subject, htmlBody, true, attachments);

    public async Task DoSendEmail(string to, string subject, string body, bool isHtml, IList<FileStream>? attachments = null)
    {
        using var smtp = new SmtpClient(_settings.Host, _settings.Port);
        using var msg = new MailMessage(_settings.Username, to, subject, body);

        smtp.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
        smtp.EnableSsl = _settings.EnableSSL;

        msg.Sender = new MailAddress(_settings.Username);
        msg.IsBodyHtml = isHtml;

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
                throw;
        }
    }
}

internal class SmtpSettings
{
    /// <summary>
    /// Gets or sets the smtp host name
    /// </summary>
    public string Host { get; set; } = null!;

    /// <summary>
    /// Gets or sets the smtp server port
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets whether the smtp server uses ssl/tls
    /// </summary>
    public bool EnableSSL { get; set; } = true;

    /// <summary>
    /// Gets or sets the username used to login to the smtp server
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password used to login to the smtp server
    /// </summary>
    public string Password { get; set; } = null!;
}
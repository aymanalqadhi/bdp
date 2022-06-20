namespace BDP.Domain.Services;

public interface IEmailService
{
    /// <summary>
    /// Asynchronously sends a plain-text email to the supplied address
    /// </summary>
    /// <param name="to">The email to send to</param>
    /// <param name="subject">The subject of the email message</param>
    /// <param name="body">The body of the email message</param>
    /// <param name="attachments">The list of attachments to send along with the email</param>
    /// <returns></returns>
    Task SendEmail(string to, string subject, string body, IList<FileStream>? attachments = null);

    /// <summary>
    /// Asynchronously sends a html-formatted email to the supplied address
    /// </summary>
    /// <param name="to">The email to send to</param>
    /// <param name="subject">The subject of the email message</param>
    /// <param name="htmlBody">The html body of the email message</param>
    /// <param name="attachments">The list of attachments to send along with the email</param>
    /// <returns></returns>
    Task SendHtmlEmail(string to, string subject, string htmlBody, IList<FileStream>? attachments = null);
}
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
    Task SendEmail(
        string to,
        string subject,
        string body,
        EmailOption options = EmailOption.None,
        IEnumerable<FileStream>? attachments = null);
}

[Flags]
public enum EmailOption : int
{
    None = 0,
    HasHtmlBody = 1,
}
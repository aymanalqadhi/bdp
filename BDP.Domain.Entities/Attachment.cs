namespace BDP.Domain.Entities;

public class Attachment : AuditableEntity
{
    /// <summary>
    /// Gets or sets the full URI of the attachment
    /// </summary>
    public string FullPath { get; set; } = null!;

    /// <summary>
    /// Gets the filename of the attachment (prior to uploading)
    /// </summary>
    public string Filename { get; set; } = null!;

    /// <summary>
    /// Gets or sets the mime reading of the attachment
    /// </summary>
    public string Mime { get; set; } = null!;

    /// <summary>
    /// Gets or sets the size of the attachment in bytes
    /// </summary>
    public long Size { get; set; }

    public bool IsImage()
        => Mime.StartsWith("image/");
}
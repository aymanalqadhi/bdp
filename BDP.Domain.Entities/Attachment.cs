namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a file attachment
/// </summary>
public class Attachment : AuditableEntity<Attachment>
{
    /// <summary>
    /// Gets or sets the full URI of the attachment
    /// </summary>
    public Uri FullPath { get; set; } = null!;

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
    public ulong Size { get; set; }
}
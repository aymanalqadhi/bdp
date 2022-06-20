namespace BDP.Domain.Entities;

/// <summary>
/// A recrod to represent keys for the <see cref="Attachment"/> entity
/// </summary>
/// <param name="Id">The id field of the key</param>
public sealed record AttachmentKey(Guid Id) : EntityKey<Attachment>(Id);

/// <summary>
/// A class to represent a file attachment
/// </summary>
public class Attachment : AuditableEntity
{
    /// <summary>
    /// Gets or sets the id of the attachment
    /// </summary>
    public AttachmentKey Id { get; set; } = new AttachmentKey(Guid.NewGuid());

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
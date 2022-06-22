using BDP.Domain.Entities;
using BDP.Domain.Repositories;
using BDP.Domain.Services;
using Winista.Mime;

namespace BDP.Infrastructure.Services;

public class LocalAttachmentsService : IAttachmentsService
{
    private readonly IUnitOfWork _uow;
    private readonly AttachmentsSettings _settings = new();

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">Application unit of work</param>
    /// <param name="configSvc">configuration service</param>
    public LocalAttachmentsService(
        IUnitOfWork uow,
        IConfigurationService configSvc)
    {
        _uow = uow;
        configSvc.Bind(nameof(AttachmentsSettings), _settings);
    }

    /// <inheritdoc/>
    public async Task<Attachment> SaveAsync(IUploadFile file)
    {
        var untrustedName = Path.GetFileName(file.FileName);
        var newPath = new Uri(Path.Combine(_settings.UploadPath, GetUniqueFilename(untrustedName)));

        if (!File.Exists(_settings.UploadPath))
            Directory.CreateDirectory(_settings.UploadPath);

        var actualPath = Path.Combine("wwwroot", newPath.LocalPath);

        using (var fs = File.OpenWrite(actualPath))
            await file.CopyToAsync(fs);

        try
        {
            using var fs = File.OpenRead(actualPath);

            var attachment = new Attachment
            {
                Filename = untrustedName,
                FullPath = newPath,
                Mime = await GetMimeFromStreamAsync(fs),
                Size = file.Length,
            };

            _uow.Attachments.Add(attachment);
            await _uow.CommitAsync();

            return attachment;
        }
        catch
        {
            File.Delete(newPath.AbsolutePath);
            throw;
        }
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<Attachment> SaveAllAsync(IEnumerable<IUploadFile> files)
    {
        foreach (var file in files)
            yield return await SaveAsync(file);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Uri path)
    {
        var attachment = await _uow.Attachments.Query().FirstOrDefaultAsync(a => a.FullPath == path);

        if (attachment == null)
            throw new FileNotFoundException("attachment not found", path.ToString());

        File.Delete(path.ToString());
        _uow.Attachments.Remove(attachment);

        await _uow.CommitAsync();
    }

    private static string GetUniqueFilename(string filename)
    {
        var ext = Path.GetExtension(filename);
        var nowTicks = DateTime.Now.Ticks;
        var guid = Guid.NewGuid().ToString("N");

        return $"{guid}.{nowTicks}{ext}";
    }

    /// <summary>
    /// Asynchronously reads the mime data from file header
    /// </summary>
    /// <param name="fs">The file stream</param>
    /// <returns>The mime string</returns>
    public static async Task<string> GetMimeFromStreamAsync(Stream fs)
    {
        var buffer = new byte[256];
        await fs.ReadAsync(buffer.AsMemory(0, 256));

        try
        {
            var mimeTypes = new MimeTypes();
            var mime = mimeTypes.GetMimeType(buffer);

            return mime.ToString();
        }
        catch
        {
            return "unknown/unknown";
        }
    }
}

internal class AttachmentsSettings
{
    /// <summary>
    /// Gets or sets the upload path
    /// </summary>
    public string UploadPath { get; set; } = null!;

    /// <summary>
    /// Gets or sets the maximum file size of uploads
    /// </summary>
    public ulong MaxFileSize { get; set; }
}
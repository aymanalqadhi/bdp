using BDP.Domain.Entities;

namespace BDP.Domain.Services;

public interface IAttachmentsService
{
    /// <summary>
    /// Asynchronously saves a file into files backend
    /// </summary>
    /// <param name="file">The file to save</param>
    /// <returns>The saved file entry</returns>
    Task<Attachment> SaveAsync(IUploadFile file);

    /// <summary>
    /// Asynchronously uploads all passed files
    /// </summary>
    /// <param name="files">The files to upload</param>
    /// <returns>The uploaded files</returns>
    IAsyncEnumerable<Attachment> SaveAllAsync(IEnumerable<IUploadFile> files);

    /// <summary>
    /// Asynchronously deletes a file from files backend
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns></returns>
    Task DeleteAsync(string path);
}

public interface IUploadFile
{
    /// <summary>
    /// Gets the file length in bytes.
    /// </summary>
    long Length { get; }

    /// <summary>
    /// Gets the file name from the Content-Disposition header.
    /// </summary>
    string FileName { get; }

    /// <summary>
    /// Asynchronously copies the uploaded file to the specified target stream
    /// </summary>
    /// <param name="target">
    ///     The target stream into which to copy the contents of
    ///     the uploaded file
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CopyToAsync(
        Stream target,
        CancellationToken cancellationToken = default);
}
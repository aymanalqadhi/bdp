using BDP.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BDP.Web.Dtos.Requests;

public class WebUploadFile : IUploadFile
{
    private readonly IFormFile _file;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="file">Inner wrapped file</param>
    public WebUploadFile(IFormFile file)
    {
        _file = file;
    }

    /// <inheritdoc/>
    public long Length => _file.Length;

    /// <inheritdoc/>
    public string FileName => _file.FileName;

    /// <inheritdoc/>
    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        => _file.CopyToAsync(target, cancellationToken);
}

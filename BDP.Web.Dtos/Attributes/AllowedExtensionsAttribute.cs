using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BDP.Web.Dtos.Attributes;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(params string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (!_extensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                return new ValidationResult(GetErrorMessage());
        }
        else if (value is IList<IFormFile> files && files.Any(f =>
             !_extensions.Contains(Path.GetExtension(f.FileName).ToLower())))
        {
            return new ValidationResult(GetErrorMessage());
        }

        return ValidationResult.Success;
    }

    public string GetErrorMessage()
    {
        return $"Only the extensions ${string.Join(", ", _extensions)} are allowed";
    }
}
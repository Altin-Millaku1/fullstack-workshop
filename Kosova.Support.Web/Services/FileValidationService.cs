using Kosova.Support.Web.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Kosova.Support.Web.Services;

public class FileValidationService(IConfiguration configuration) : IFileValidationService
{
    private static readonly HashSet<string> AllowedExtensions = [".pdf", ".jpg", ".jpeg", ".png"];

    public Task<(bool IsValid, string ErrorMessage)> ValidateAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var maxBytes = configuration.GetValue<long>("Upload:MaxBytes", 5 * 1024 * 1024);
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
        {
            return Task.FromResult((false, "Unsupported file extension."));
        }

        if (file.Length <= 0 || file.Length > maxBytes)
        {
            return Task.FromResult((false, "Invalid file size."));
        }

        // Virus scanning integration placeholder: invoke external scanner service here.
        return Task.FromResult((true, string.Empty));
    }
}

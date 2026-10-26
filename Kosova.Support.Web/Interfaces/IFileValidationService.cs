using Microsoft.AspNetCore.Http;

namespace Kosova.Support.Web.Interfaces;

public interface IFileValidationService
{
    Task<(bool IsValid, string ErrorMessage)> ValidateAsync(IFormFile file, CancellationToken cancellationToken = default);
}

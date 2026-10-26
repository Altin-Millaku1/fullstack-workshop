using Kosova.Support.Core.Interfaces;

namespace Kosova.Support.Infrastructure.Services;

public class PlaceholderFileScanner : IFileScanner
{
    public Task<bool> IsCleanAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }
}

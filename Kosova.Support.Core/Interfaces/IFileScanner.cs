namespace Kosova.Support.Core.Interfaces;

public interface IFileScanner
{
    Task<bool> IsCleanAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
}

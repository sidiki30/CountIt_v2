using CountIt.Interfaces;

namespace CountIt;

public class FileWrapper : IFileWrapper
{
    public bool Exists(string filePath)
    {
        return File.Exists(filePath);
    }

    public Task<string> ReadAllTextAsync(string filePath)
    {
        return File.ReadAllTextAsync(filePath);
    }
}

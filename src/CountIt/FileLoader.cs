using CountIt.Interfaces;

namespace CountIt;

public class FileLoader : IFileLoader
{
    private readonly IFileWrapper _fileWrapper;

    public FileLoader(IFileWrapper fileWrapper)
    {
        _fileWrapper = fileWrapper;
    }

    public async Task<string> LoadFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.");
        }

        if (!_fileWrapper.Exists(filePath))
        {
            throw new FileNotFoundException($"The file {filePath} does not exist.");
        }

        try
        {
            return await _fileWrapper.ReadAllTextAsync(filePath);
        }
        catch (Exception ex)
        {
            throw new FileLoadException("An error occurred while reading the file.", ex);
        }
    }
}

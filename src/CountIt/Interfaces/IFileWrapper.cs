namespace CountIt.Interfaces;

public interface IFileWrapper
{
    bool Exists(string filePath);
    Task<string> ReadAllTextAsync(string filePath);
}

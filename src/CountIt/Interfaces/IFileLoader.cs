namespace CountIt.Interfaces;

public interface IFileLoader
{
    Task<string> LoadFileAsync(string filePath);
}

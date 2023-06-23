namespace CountIt.Interfaces;

public interface IDocumentProcessor
{
    Task<Dictionary<string, int>> ProcessFileAsync(string filePath);
}

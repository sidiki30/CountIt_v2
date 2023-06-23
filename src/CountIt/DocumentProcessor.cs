using CountIt.Interfaces;
using Serilog;

namespace CountIt;

public class DocumentProcessor : IDocumentProcessor
{
    private readonly IFileLoader _fileLoader;
    private readonly IWordCounter _wordCounter;
    private readonly ISorter _sorter;
    private readonly ILogger _logger;

    public DocumentProcessor(IFileLoader fileLoader, IWordCounter wordCounter, ISorter sorter, ILogger logger)
    {
        _fileLoader = fileLoader;
        _wordCounter = wordCounter;
        _sorter = sorter;
        _logger = logger;
    }

    public async Task<Dictionary<string, int>> ProcessFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.");
        }

        var result = new Dictionary<string, int>();

        try
        {
            var text = await _fileLoader.LoadFileAsync(filePath);
            var wordCountDictionary = _wordCounter.CountWords(text);
            result = _sorter.SortByKeys(wordCountDictionary);
        }
        catch (FileNotFoundException ex)
        {
            _logger.Information($"The file '{filePath}' does not exist.", ex);
        }
        catch (FileLoadException ex)
        {
            _logger.Information("An error occurred while reading the file.", ex);
        }
        return result;
    }
}

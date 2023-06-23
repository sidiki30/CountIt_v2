using CountIt.Interfaces;
using FluentAssertions;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace CountIt.UnitTests;

public class DocumentProcessorTests
{
    private readonly Mock<IFileLoader> _fileLoaderMock;
    private readonly Mock<ILogger> _loggerMock;
    private readonly DocumentProcessor _documentProcessor;

    public DocumentProcessorTests()
    {
        _fileLoaderMock = new Mock<IFileLoader>();
        _loggerMock = new Mock<ILogger>();

        _documentProcessor = new DocumentProcessor(
            _fileLoaderMock.Object,
            new WordCounter(),
            new AlphabeticalSorter(),
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task ProcessFileAsync_ThrowsArgumentException_WhenFilePathUndefined()
    {
        // Arrange
        var filePath = string.Empty;
        var errorMessage = $"'{nameof(filePath)}' cannot be null or whitespace.";

        //Act
        var act = async () => await _documentProcessor.ProcessFileAsync(filePath);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(errorMessage);
        _fileLoaderMock.Verify(x => x.LoadFileAsync(filePath), Times.Never);
    }

    [Fact]
    public async Task ProcessFileAsync_ReturnsSortedWordAlphabetically_WhenFileContentProvided()
    {
        // Arrange
        var filePath = "test.txt";
        var fileContent = "The big brown fox number 4 jumped over the lazy dog. THE BIG BROWN FOX JUMPED OVER THE LAZY DOG. The Big Brown Fox 123.";

        var expectedWordCounts = new Dictionary<string, int>
            {
                { "big", 3 },
                { "brown", 3 },
                { "dog", 2 },
                { "fox", 3 },
                { "jumped", 2 },
                { "lazy", 2 },
                { "number", 1 },
                { "over", 2 },
                { "the", 5 }
            };

        _fileLoaderMock.Setup(f => f.LoadFileAsync(filePath)).ReturnsAsync(fileContent);

        // Act
        var result = await _documentProcessor.ProcessFileAsync(filePath);

        // Assert
        result.Should().BeEquivalentTo(expectedWordCounts);
        _fileLoaderMock.Verify(x => x.LoadFileAsync(filePath), Times.Once);
    }

    [Fact]
    public async Task ProcessFileAsync_WhenFileDoesNotExist_ReturnsEmptyDictionary()
    {
        // Arrange
        var filePath = "nonexistentfile.txt";
        var logMessage = $"The file '{filePath}' does not exist.";
        var exception = new FileNotFoundException();

        _fileLoaderMock.Setup(f => f.LoadFileAsync(filePath)).Throws(exception);

        // Act
        var result = await _documentProcessor.ProcessFileAsync(filePath);

        // Assert
        result.Should().BeEmpty();
        _loggerMock.Verify(x => x.Information(logMessage, exception), Times.Once);
        _fileLoaderMock.Verify(x => x.LoadFileAsync(filePath), Times.Once);
    }

    [Fact]
    public async Task ProcessFileAsync_WhenFileLoadFails_ReturnsEmptyDictionary()
    {
        // Arrange
        var filePath = "test.txt";
        var logMessage = "An error occurred while reading the file.";
        var exception = new FileLoadException();

        _fileLoaderMock.Setup(f => f.LoadFileAsync(filePath)).Throws(exception);

        // Act
        var result = await _documentProcessor.ProcessFileAsync(filePath);

        // Assert
        result.Should().BeEmpty();
        _loggerMock.Verify(x => x.Information(logMessage, exception), Times.Once);
        _fileLoaderMock.Verify(x => x.LoadFileAsync(filePath), Times.Once);
    }
}

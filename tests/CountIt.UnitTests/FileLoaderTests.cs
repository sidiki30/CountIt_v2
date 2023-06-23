using CountIt.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace CountIt.UnitTests;

public class FileLoaderTests
{
    private const string FilePath = "test.txt";

    private readonly Mock<IFileWrapper> _fileWrapperMock;
    private readonly FileLoader _fileLoader;

    public FileLoaderTests()
    {
        _fileWrapperMock = new Mock<IFileWrapper>();
        _fileWrapperMock.Setup(x => x.Exists(FilePath)).Returns(true);

        _fileLoader = new FileLoader(_fileWrapperMock.Object);
    }

    [Fact]
    public async Task LoadFileAsync_ReturnsFileContent_WhenFileExists()
    {
        // Arrange
        var expectedContent = "The big brown fox number 4 jumped over the lazy dog. THE BIG BROWN FOX JUMPED OVER THE LAZY DOG. The Big Brown Fox 123.";

        _fileWrapperMock.Setup(x => x.ReadAllTextAsync(FilePath)).ReturnsAsync(expectedContent);

        // Act
        var result = await _fileLoader.LoadFileAsync(FilePath);

        // Assert
        result.Should().BeEquivalentTo(expectedContent);
        _fileWrapperMock.Verify(x => x.Exists(FilePath), Times.Once);
        _fileWrapperMock.Verify(x => x.ReadAllTextAsync(FilePath), Times.Once);
    }

    [Fact]
    public async Task LoadFileAsync_ThrowsArgumentException_WhenFilePathUndefined()
    {
        // Arrange
        var filePath = string.Empty;
        var errorMessage = $"'{nameof(filePath)}' cannot be null or whitespace.";

        //Act
        var act = async () => await _fileLoader.LoadFileAsync(string.Empty);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(errorMessage);
        _fileWrapperMock.Verify(x => x.Exists(FilePath), Times.Never);
        _fileWrapperMock.Verify(x => x.ReadAllTextAsync(FilePath), Times.Never);
    }

    [Fact]
    public async Task LoadFileAsync_ThrowsFileNotFoundException_WhenFileDoesNotExist()
    {
        // Arrange
        var filePath = "nonexistent_file.txt";
        var errorMessage = $"The file {filePath} does not exist.";
        _fileWrapperMock.Setup(x => x.Exists(filePath)).Returns(false);

        //Act
        var act = async () => await _fileLoader.LoadFileAsync(filePath);

        // Assert
        await act.Should().ThrowAsync<FileNotFoundException>().WithMessage(errorMessage);
        _fileWrapperMock.Verify(x => x.Exists(filePath), Times.Once);
    }

    [Fact]
    public async Task LoadFileAsync_ThrowsFileLoadException_WhenTheFileCannotBeLoaded()
    {
        // Arrange
        var exception = new IOException("Read error");
        _fileWrapperMock.Setup(x => x.ReadAllTextAsync(FilePath)).ThrowsAsync(exception);

        // Act
        var act = async () => await _fileLoader.LoadFileAsync(FilePath);

        // Assert
        await act.Should()
            .ThrowAsync<FileLoadException>()
            .WithMessage("An error occurred while reading the file.");
        _fileWrapperMock.Verify(x => x.Exists(FilePath), Times.Once);
        _fileWrapperMock.Verify(x => x.ReadAllTextAsync(FilePath), Times.Once);
    }
}

using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace CountIt.UnitTests;

public class WordCounterTests
{
    private readonly WordCounter _wordCounter;

    public WordCounterTests()
    {
        _wordCounter = new WordCounter();
    }

    [Fact]
    public void CountWords_ReturnsEmptyDictionary_WhenTextContentIsEmpty()
    {
        // Arrange && Act
        var result = _wordCounter.CountWords(string.Empty);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void CountWords_ReturnsEmptyDictionary_WhenTextContentIsNull()
    {
        // Arrange && Act
        var result = _wordCounter.CountWords(default);

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("apple apple banana orange banana apple!")]
    [InlineData("apple APPLE banana ORANGE BANANA apple.")]
    [InlineData("apple apple 123 banana orange banana apple 2563")]
    [InlineData("apple apple banana orange\t banana apple\r\n?")]
    public void CountWords_ReturnsCorrectWordCounts_WhenTextContentProvided(string text)
    {
        // Arrange
        var expectedWordCounts = new Dictionary<string, int>
            {
                { "apple", 3 },
                { "banana", 2 },
                { "orange", 1 }
            };

        // Act
        var result = _wordCounter.CountWords(text);

        // Assert
        result.Should().BeEquivalentTo(expectedWordCounts);
    }
}

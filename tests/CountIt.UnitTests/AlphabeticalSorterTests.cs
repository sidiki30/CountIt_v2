using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace CountIt.UnitTests;

public class AlphabeticalSorterTests
{
    private readonly AlphabeticalSorter _sorter;

    public AlphabeticalSorterTests()
    {
        _sorter = new AlphabeticalSorter();
    }

    [Fact]
    public void SortByKeys_ReturnsEmptyDictionary_WhenGivenEmptyDictionary()
    {
        // Arrange
        var dictionary = new Dictionary<string, int>();

        // Act
        var results = _sorter.SortByKeys(dictionary);

        // Assert
        results.Should().BeEmpty();
    }

    [Fact]
    public void SortByKeys_ReturnsDictionarySortedByKeysAscending_WhenGivenDictionaryIsUnsorted()
    {
        // Arrange
        var dictionary = new Dictionary<string, int>
        {
            { "apple", 2 },
            { "dog", 4 },
            { "brown", 3 },
            { "Amsterdam", 1 }
        };

        var expected = new Dictionary<string, int>
        {
            { "Amsterdam", 1 },
            { "apple", 2 },
            { "brown", 3 },
            { "dog", 4 }
        };

        // Act
        var results = _sorter.SortByKeys(dictionary);

        // Assert
        results.Should().BeEquivalentTo(expected);
    }
}
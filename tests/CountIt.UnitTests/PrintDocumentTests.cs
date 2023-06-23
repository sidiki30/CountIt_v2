using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace CountIt.UnitTests;

public class PrintDocumentTests
{
    [Fact]
    public void PrintToConsole_ShouldOutputCorrectResult()
    {
        // Arrange
        var printDocument = new PrintDocument();
        var wordCountMap = new Dictionary<string, int>
        {
            { "apple", 2 },
            { "banana", 3 },
            { "orange", 1 }
        };

        var expected = $"Number of words: {wordCountMap.Sum(x => x.Value)}\n\r\n" +
            "apple 2\r\n" +
            "banana 3\r\n" +
            "orange 1\r\n";

        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        printDocument.PrintToConsole(wordCountMap);
        var result = consoleOutput.ToString();

        // Assert
        result.Should().Be(expected);
    }
}

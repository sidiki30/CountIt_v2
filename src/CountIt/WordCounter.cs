using CountIt.Interfaces;
using System.Text.RegularExpressions;

namespace CountIt;

public class WordCounter : IWordCounter
{
    public Dictionary<string, int> CountWords(string text)
    {
        var wordCountDictionary = new Dictionary<string, int>();

        if (string.IsNullOrWhiteSpace(text))
        {
            return wordCountDictionary;
        }

        var regex = new Regex(@"\b[A-Za-z]+\b");
        var matches = regex.Matches(text.ToLower());
        var words = matches.Select(x => x.Value);

        foreach (var word in words)
        {
            if (!wordCountDictionary.ContainsKey(word))
            {
                wordCountDictionary[word] = 0;
            }

            wordCountDictionary[word]++;
        }

        return wordCountDictionary;
    }
}


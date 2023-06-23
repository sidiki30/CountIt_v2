using CountIt.Interfaces;

namespace CountIt;

public class AlphabeticalSorter : ISorter
{
    public Dictionary<string, int> SortByKeys(IDictionary<string, int> dictionary)
    {
        var wordCountPairs = dictionary.ToList();
        var totalWordCountPairs = wordCountPairs.Count;

        for (var i = 0; i < totalWordCountPairs - 1; i++)
        {
            for (var j = 0; j < totalWordCountPairs - i - 1; j++)
            {
                if (string.Compare(wordCountPairs[j].Key, wordCountPairs[j + 1].Key) > 0)
                {
                    var tempPair = wordCountPairs[j];
                    wordCountPairs[j] = wordCountPairs[j + 1];
                    wordCountPairs[j + 1] = tempPair;
                }
            }
        }

        return wordCountPairs.ToDictionary(k => k.Key, v => v.Value);
    }
}
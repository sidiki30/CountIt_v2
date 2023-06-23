using CountIt.Interfaces;

namespace CountIt;

public class PrintDocument : IPrintDocument
{
    public void PrintToConsole(Dictionary<string, int> wordCountMap)
    {
        Console.WriteLine($"Number of words: {wordCountMap.Sum(x => x.Value)}\n");

        foreach (var wordCountMapItem in wordCountMap)
        {
            Console.WriteLine($"{wordCountMapItem.Key} {wordCountMapItem.Value}");
        }
    }
}

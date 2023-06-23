namespace CountIt.Interfaces;

public interface IPrintDocument
{
    void PrintToConsole(Dictionary<string, int> wordCountMap);
}
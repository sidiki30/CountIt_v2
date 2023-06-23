namespace CountIt.Interfaces;

public interface IWordCounter
{
    Dictionary<string, int> CountWords(string text);
}

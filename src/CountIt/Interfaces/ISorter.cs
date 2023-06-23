namespace CountIt.Interfaces;

public interface ISorter
{
    Dictionary<string, int> SortByKeys(IDictionary<string, int> dictionary);
}
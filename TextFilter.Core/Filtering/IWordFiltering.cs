namespace TextFilter.Core.Filtering;

public interface IWordFilter
{
    bool ShouldFilter(string word);
}

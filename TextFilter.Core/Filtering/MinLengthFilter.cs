namespace TextFilter.Core.Filtering;

public class MinLengthFilter : IWordFilter
{
    private readonly int _min;

    public MinLengthFilter(int min = 3)
    {
        _min = min;
    }
    public bool ShouldFilter(string word){
        return string.IsNullOrWhiteSpace(word) || word.Trim().Length < _min;
    }
}

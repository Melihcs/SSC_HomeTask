namespace TextFilter.Core.Filtering;

public sealed class ContainsLetterFilter : IWordFilter
{
    private readonly char _letter;
    public ContainsLetterFilter(char letter = 't')
    {
        _letter = char.ToLowerInvariant(letter);
    }
    public bool ShouldFilter(string word)
    {
            return !string.IsNullOrWhiteSpace(word) &&
           word.Contains(_letter, StringComparison.OrdinalIgnoreCase);
        }
}

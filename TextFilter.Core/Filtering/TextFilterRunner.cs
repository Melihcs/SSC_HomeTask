using System.Text;

namespace TextFilter.Core.Filtering;

/// <summary>
/// Applies multiple text filters to process text, removing filtered words.
/// </summary>
public class TextFilterRunner
{
    private List<IWordFilter> _filters = new();
    private readonly TextTokenizer _tokenizer;
    public TextFilterRunner() : this(new TextTokenizer())
    {
    }
    public TextFilterRunner(TextTokenizer tokenizer)
    {
        _tokenizer = tokenizer ?? throw new ArgumentNullException(nameof(tokenizer));
    }

    public void AddFilter(IWordFilter filter)
    {
        if (filter == null)
            throw new ArgumentNullException(nameof(filter));

        _filters.Add(filter);
    }

    public void AddFilters(IEnumerable<IWordFilter> filters)
    {
        if (filters == null)
            throw new ArgumentNullException(nameof(filters));

        foreach (var filter in filters)
        {
            AddFilter(filter);
        }
    }

    public string ApplyFilters(string text)
    {
        if (string.IsNullOrEmpty(text) || _filters.Count == 0)
            return text;

        var tokens = _tokenizer.TokenizeWithPositions(text);
        var result = new StringBuilder(text);

        var tokensToRemove = tokens
            .Where(token => ShouldFilterWord(token.Word))
            .OrderByDescending(token => token.StartIndex)
            .ToList();

        foreach (var token in tokensToRemove)
        {
            result.Remove(token.StartIndex, token.Length);
        }

        return result.ToString();
    }

    private bool ShouldFilterWord(string word)
    {
        return _filters.Any(filter => filter.ShouldFilter(word));
    }

    private static string CleanWhitespace(string text)
    {
        //In case we need to clear the spaces after filering.
        var result = System.Text.RegularExpressions.Regex.Replace(text, @"\s{2,}", " ");
        var lines = result.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Trim();
        }
        return string.Join("\n", lines).Trim();
    }
}

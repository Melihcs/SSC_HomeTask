using System.Text.RegularExpressions;
namespace TextFilter.Core.Filtering;

public class TextTokenizer
{
    //words with optional ' like don't. did not countains words with numbers though"
    private static readonly Regex WordPattern = new(@"[a-zA-Z]+(?:'[a-zA-Z]+)?", RegexOptions.Compiled);

    public List<WordToken> TokenizeWithPositions(string text)
    {
        if (string.IsNullOrEmpty(text))
            return new List<WordToken>();

        var matches = WordPattern.Matches(text);
        return matches.Select(m => new WordToken(m.Value, m.Index, m.Length)).ToList();
    }
}

public record WordToken(string Word, int StartIndex, int Length);

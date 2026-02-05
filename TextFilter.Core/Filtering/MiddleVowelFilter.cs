namespace TextFilter.Core.Filtering;

public class MiddleVowelFilter : IWordFilter
{
    private static readonly HashSet<char> Vowels = new() { 'a', 'e', 'i', 'o', 'u' };

    public bool ShouldFilter(string word)
    {
        var trimmedWord = word?.Trim();
        if (string.IsNullOrWhiteSpace(trimmedWord))
        {
            return true;
        }
        return trimmedWord.Length % 2 == 1?
            ShouldFilterForSingle(trimmedWord):
            ShouldFilterForDouble(trimmedWord);
        
    }
    private bool ShouldFilterForSingle(string word)
    {
        var mid = char.ToLowerInvariant(word[word.Length / 2]);
            return Vowels.Contains(mid);
    }
    private bool ShouldFilterForDouble(string word)
    {
        int rightMid = word.Length / 2;
            int leftMid = rightMid - 1;

            var c1 = char.ToLowerInvariant(word[leftMid]);
            var c2 = char.ToLowerInvariant(word[rightMid]);

            return Vowels.Contains(c1) || Vowels.Contains(c2);
    }
}

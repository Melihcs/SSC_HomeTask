using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextFilter.Core.Filtering;

namespace TextFilter.Tests;

[TestClass]
public class ContainsLetterFilterTests
{
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ShouldFilter_ReturnsFalse_ForNullOrWhitespace(string? word)
    {
        var filter = new ContainsLetterFilter('t');
        var result = filter.ShouldFilter(word!);
        Assert.IsFalse(result);
    }

    [DataTestMethod]
    [DataRow("table", 't', true)]
    [DataRow("TABLE", 't', true)]
    [DataRow("chair", 't', false)]
    [DataRow("alpha", 'a', true)]
    [DataRow("don't", 't', true)]
    [DataRow("num1ber", 'e', true)]
    public void ShouldFilter_IsCaseInsensitive(string word, char letter, bool expected)
    {
        var filter = new ContainsLetterFilter(letter);
        var result = filter.ShouldFilter(word);
        Assert.AreEqual(expected, result);
    }
}

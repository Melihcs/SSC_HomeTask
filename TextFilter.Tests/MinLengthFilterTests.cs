using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextFilter.Core.Filtering;

namespace TextFilter.Tests;

[TestClass]
public class MinLengthFilterTests
{
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ShouldFilter_ReturnsTrue_ForNullOrWhitespace(string? word)
    {
        var filter = new MinLengthFilter(3);

        var result = filter.ShouldFilter(word!);

        Assert.IsTrue(result);
    }

    [DataTestMethod]
    [DataRow("hi", 3, true)]
    [DataRow("hey", 3, false)]
    [DataRow("  ok ", 3, true)]
    [DataRow("  good ", 3, false)]
    [DataRow("he's", 3, false)]
    [DataRow("nu1m", 5, true)]

    public void ShouldFilter_RespectsMinimumLength(string word, int min, bool expected)
    {
        var filter = new MinLengthFilter(min);

        var result = filter.ShouldFilter(word);

        Assert.AreEqual(expected, result);
    }
}

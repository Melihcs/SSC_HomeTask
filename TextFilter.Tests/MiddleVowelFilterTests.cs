using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextFilter.Core.Filtering;

namespace TextFilter.Tests;

[TestClass]
public class MiddleVowelFilterTests
{
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void ShouldFilter_ReturnsTrue_ForNullOrWhitespace(string? word)
    {
        var filter = new MiddleVowelFilter();

        var result = filter.ShouldFilter(word!);

        Assert.IsTrue(result);
    }

    [DataTestMethod]
    [DataRow("cat", true)]
    [DataRow("cbc", false)]
    [DataRow("bAb", true)]
    [DataRow("doe't", true)]

    public void ShouldFilter_OddLength_WorksOnSingleMiddleChar(string word, bool expected)
    {
        var filter = new MiddleVowelFilter();

        var result = filter.ShouldFilter(word);

        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow("door", true)]
    [DataRow("ttbb", false)]
    [DataRow("tESt", true)]
    [DataRow("do't", true)]
    [DataRow("numi1ber", true)]

    public void ShouldFilter_EvenLength_WorksOnTwoMiddleChars(string word, bool expected)
    {
        var filter = new MiddleVowelFilter();

        var result = filter.ShouldFilter(word);

        Assert.AreEqual(expected, result);
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextFilter.Core.Filtering;

namespace TextFilter.Tests;

[TestClass]
public class TextFilterRunnerTests
{
    [TestMethod]
    public void AddFilter_ThrowsForNull()
    {
        var runner = new TextFilterRunner();
        Assert.ThrowsException<ArgumentNullException>(() => runner.AddFilter(null!));
    }

    [TestMethod]
    public void AddFilters_ThrowsForNull()
    {
        var runner = new TextFilterRunner();
        Assert.ThrowsException<ArgumentNullException>(() => runner.AddFilters(null!));
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    public void ApplyFilters_ReturnsOriginal_WhenTextNullOrEmpty(string? text)
    {
        var runner = new TextFilterRunner();
        runner.AddFilter(new MinLengthFilter(3));

        var result = runner.ApplyFilters(text!);

        if (text == null)
        {
            Assert.IsNull(result);
        }
        else
        {
            Assert.AreEqual(text, result);
        }
    }

    [TestMethod]
    public void ApplyFilters_ReturnsOriginal_WhenNoFilters()
    {
        var runner = new TextFilterRunner();
        var text = "original Text";

        var result = runner.ApplyFilters(text);

        Assert.AreEqual(text, result);
    }

    [TestMethod]
    public void ApplyFilters_RemovesFilteredWords_PreservingOtherText()
    {
        var runner = new TextFilterRunner();
        runner.AddFilter(new ContainsLetterFilter('m'));

        var text = "test text for me";
        var result = runner.ApplyFilters(text);

        Assert.AreEqual("test text for ", result);
    }

    [TestMethod]
    public void ApplyFilters_CombinesMultipleFilters_WithOrBehavior()
    {
        var runner = new TextFilterRunner();
        runner.AddFilters(new IWordFilter[]
        {
            new MinLengthFilter(4),
            new ContainsLetterFilter('t')
        });

        var text = "one two three four";
        var result = runner.ApplyFilters(text);

        Assert.AreEqual("   four", result);
    }
}

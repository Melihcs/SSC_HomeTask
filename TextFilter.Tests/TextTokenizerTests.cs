using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextFilter.Core.Filtering;

namespace TextFilter.Tests;

[TestClass]
public class TextTokenizerTests
{
    [TestMethod]
    public void TokenizeWithPositions_ReturnsEmpty_ForNullOrEmptyText()
    {
        var tokenizer = new TextTokenizer();

        var emptyTokens = tokenizer.TokenizeWithPositions("");
        var nullTokens = tokenizer.TokenizeWithPositions(null!);

        Assert.AreEqual(0, emptyTokens.Count);
        Assert.AreEqual(0, nullTokens.Count);
    }

    [TestMethod]
    public void TokenizeWithPositions_ExtractsWordsAndPositions()
    {
        var tokenizer = new TextTokenizer();
        var text = "Don't stop 123 now.";

        var tokens = tokenizer.TokenizeWithPositions(text);

        Assert.AreEqual(3, tokens.Count);
        Assert.AreEqual(new WordToken("Don't", 0, 5), tokens[0]);
        Assert.AreEqual(new WordToken("stop", 6, 4), tokens[1]);
        Assert.AreEqual(new WordToken("now", 15, 3), tokens[2]);
    }
}

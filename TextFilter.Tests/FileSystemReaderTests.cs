using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextFilter.Core.FileOperations;

namespace TextFilter.Tests;

[TestClass]
public class FileSystemReaderTests
{
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void Ctor_ThrowsForNullOrWhitespace(string? path)
    {
        var ex = Assert.ThrowsException<ArgumentException>(() => new FileSystemReader(path!));
        StringAssert.Contains(ex.Message, "File path cannot be empty");
    }

    [TestMethod]
    public void Ctor_ThrowsForMissingFile()
    {
        var missingPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");
        var ex = Assert.ThrowsException<FileNotFoundException>(() => new FileSystemReader(missingPath));
        Assert.AreEqual(missingPath, ex.FileName);
    }

    [TestMethod]
    public void ReadFileText_ReadsUtf8File()
    {
        var path = Path.GetTempFileName();
        try
        {
            var expected = "Hello, world!";
            File.WriteAllText(path, expected, Encoding.UTF8);
            var reader = new FileSystemReader(path);
            var result = reader.ReadFileText();
            Assert.AreEqual(expected, result);
        }
        finally
        {
            File.Delete(path);
        }
    }
}

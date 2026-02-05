using System.Text;

namespace TextFilter.Core.FileOperations;

public class FileSystemReader : IFileReader
{
    private readonly string _filePath;

    public FileSystemReader(string filePath)
    {
        EnsureFilePath(filePath);
        _filePath = filePath;
    }
  public string ReadFileText()
    {
        return File.ReadAllText(_filePath, Encoding.UTF8);
    }
    private void EnsureFilePath(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be empty.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found.", filePath);
    }
}

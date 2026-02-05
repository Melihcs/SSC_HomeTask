using TextFilter.Core.FileOperations;
using TextFilter.Core.Filtering;

Console.Write("File path: ");
var path = Console.ReadLine() ?? "";

IFileReader reader = new FileSystemReader(path);
var text = reader.ReadFileText();
TextFilterRunner filterEngine = new TextFilterRunner();
filterEngine.AddFilters(new List<IWordFilter>
{
    new MiddleVowelFilter(),
    new MinLengthFilter(3),
    new ContainsLetterFilter('t')
});
var filtered = filterEngine.ApplyFilters(text);

Console.WriteLine(filtered);


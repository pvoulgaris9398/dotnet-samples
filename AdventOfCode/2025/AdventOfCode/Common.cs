namespace AdventOfCode;

public static class Common
{
    internal static IEnumerable<string> ReadLines(this TextReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        while (reader.ReadLine() is string line)
        {
            yield return line;
        }
    }

    public static List<string> ToLines(this string fileName) =>
        [.. File.OpenText(fileName).ReadLines()];

    public static TextReader ReadFromFile(this string path) => File.OpenText(path);
}

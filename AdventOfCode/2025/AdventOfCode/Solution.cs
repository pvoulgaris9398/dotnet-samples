namespace AdventOfCode;

public abstract class SolutionBase
{
    private const int Year = 2025;
    public string Source => $"Advent of Code - {Year}";
    public abstract string Name { get; }
    public abstract string FileName { get; }

    public abstract int SolvePart1(bool debug = false);

    public abstract int SolvePart2(bool debug = false);

    protected List<string> Lines => [.. File.OpenText(FileName).ReadLines()];

    protected abstract List<string> Test { get; }
}

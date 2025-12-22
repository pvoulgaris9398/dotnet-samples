namespace AdventOfCode;

public static class Matrix
{
    public static char[][] ToMatrix(this IEnumerable<string> lines) =>
        lines.Select(line => line.ToCharArray()).ToArray();

    public static bool IsOutside(this (int row, int col) cell, char[][] matrix) =>
        cell.row > matrix.Length - 1
        || cell.row < 0
        || cell.col > matrix[0].Length - 1
        || cell.col < 0;

    public static char At(this (int row, int col) cell, char[][] matrix) =>
        !IsOutside(cell, matrix) ? matrix[cell.row][cell.col] : '\0';

    public static IEnumerable<(int, int)> Cells(this char[][] matrix) =>
        from row in Enumerable.Range(0, matrix.Length)
        from column in Enumerable.Range(0, matrix[row].Length)
        select (row, column);
}

namespace Algorithms
{
    internal static class DayEight
    {
        private static string[] TestData => [
"............",
"........0...",
".....0......",
".......0....",
"....0.......",
"......A.....",
"............",
"............",
"........A...",
".........A..",
"............",
"............"
            ];

        private static List<string> RealData => CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\day8.txt");

        /// <summary>
        /// With thanks to Zoran for pointing me in the right direction...
        /// </summary>
        /// <param name="testing"></param>
        public static void Run(bool testing = false)
        {

            WriteLine(new string('*', 80));
            WriteLine($"{nameof(DayEight)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            var data = testing ? [.. TestData] : RealData;
            char[][] map = data.ReadMap();

            var antennas = GetAntennas(map).ToList();

            var count1 = antennas
                .SelectMany(a => a.GetAntinodes(map, NonResonatingAntinodes))
                .Distinct()
                .Count();

            var count2 = antennas
                .SelectMany(a => a.GetAntinodes(map, ResonatingAntinodes))
                .Distinct()
                .Count();

            WriteLine($"{nameof(count1)}: {count1}");
            WriteLine($"R{nameof(count2)}: {count2}");
        }

        private static bool IsInside(this char[][] map, Position position) =>
            position.Row >= 0 && position.Row < map.Length &&
            position.Col >= 0 && position.Col < map[0].Length;

        private static IEnumerable<Position> GetAntinodes(this AntennaSet antennas, char[][] map, AntinodeGenerator antinodeGenerator) =>
            antennas.GetPositionPairs().SelectMany(pair => map.GetAntinodes(pair.a1, pair.a2, antinodeGenerator));

        private static IEnumerable<Position> NonResonatingAntinodes(this char[][] map, Position antenna, int rowDiff, int colDiff)
        {
            var position = new Position(antenna.Row + rowDiff, antenna.Col + colDiff);
            if (map.IsInside(position))
            {
                yield return position;
            }
        }

        private static IEnumerable<Position> ResonatingAntinodes(this char[][] map, Position antenna, int rowDiff, int colDiff)
        {
            yield return antenna;
            var position = antenna;
            while (map.IsInside(position))
            {
                yield return position;
                position = new(position.Row + rowDiff, position.Col + colDiff);
            }
        }

        private static IEnumerable<Position> GetAntinodes(
            this char[][] map, Position antenna1, Position antenna2,
            AntinodeGenerator antinodeGenerator)
        {
            int rowDiff = antenna1.Row - antenna2.Row;
            int colDiff = antenna1.Col - antenna2.Col;

            return antinodeGenerator(map, antenna1, rowDiff, colDiff)
                .Concat(antinodeGenerator(map, antenna2, -rowDiff, -colDiff));
        }

        private delegate IEnumerable<Position> AntinodeGenerator(char[][] map, Position antenna, int rowDiff, int colDiff);

        private static IEnumerable<(Position a1, Position a2)> GetPositionPairs(this AntennaSet antennas) =>
            antennas.Positions.SelectMany((pos1, index1) =>
                antennas.Positions.Skip(index1 + 1).Select(pos2 => (pos1, pos2)));

        private static IEnumerable<AntennaSet> GetAntennas(this char[][] map) =>
            map.GetIndividualAntennas().GroupBy(
                antenna => antenna.Frequency,
                (frequency, antennas) => new AntennaSet(frequency, [.. antennas.Select(antenna => antenna.Position)]));

        private static IEnumerable<Antenna> GetIndividualAntennas(this char[][] map) =>
            map.SelectMany((row, rowIndex) =>
                row.Select((content, colIndex) => (content, rowIndex, colIndex))
                .Where(cell => cell.content != '.')
                .Select(cell => new Antenna(cell.content, new(cell.rowIndex, cell.colIndex))));

        // ReSharper disable once NotAccessedPositionalProperty.Local
        private sealed record AntennaSet(char Frequency, List<Position> Positions);

        private sealed record Antenna(char Frequency, Position Position);

        private sealed record Position(int Row, int Col);
    }
}
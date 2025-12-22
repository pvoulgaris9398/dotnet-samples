using System.Globalization;

namespace Advent2024
{
    internal static class Day13
    {
        private static List<string> RealData =>
            CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\data\\day13.txt");

        private static readonly List<string> TestData1 =
        [
            "Button A: X+94, Y+34\r\nButton B: X+22, Y+67\r\nPrize: X=8400, Y=5400",
        ];

        private sealed record Game(long AX, long AY, long BX, long BY, long PrizeX, long PrizeY);

        private static Game? ToGame(this string block)
        {
            var lines = block.Split('\n');

            var a = lines[0].Replace("Button A:", "", StringComparison.InvariantCulture).Split(',');
            var b = lines[1].Replace("Button B:", "", StringComparison.InvariantCulture).Split(',');
            var prize = lines[2]
                .Replace("Prize:", "", StringComparison.InvariantCulture)
                .Split(',');
            if (
                long.TryParse(
                    a[0].Replace("X+", "", StringComparison.InvariantCulture).Trim(),
                    CultureInfo.InvariantCulture.NumberFormat,
                    out long ax
                )
                && long.TryParse(
                    a[1].Replace("Y+", "", StringComparison.InvariantCulture).Trim(),
                    CultureInfo.InvariantCulture.NumberFormat,
                    out long ay
                )
                && long.TryParse(
                    b[0].Replace("X+", "", StringComparison.InvariantCulture).Trim(),
                    CultureInfo.InvariantCulture.NumberFormat,
                    out long bx
                )
                && long.TryParse(
                    b[1].Replace("Y+", "", StringComparison.InvariantCulture).Trim(),
                    CultureInfo.InvariantCulture.NumberFormat,
                    out long by
                )
                && long.TryParse(
                    prize[0].Replace("X=", "", StringComparison.InvariantCulture).Trim(),
                    CultureInfo.InvariantCulture.NumberFormat,
                    out long prizeX
                )
                && long.TryParse(
                    prize[1].Replace("Y=", "", StringComparison.InvariantCulture).Trim(),
                    CultureInfo.InvariantCulture.NumberFormat,
                    out long prizeY
                )
            )
            {
                return new(ax, ay, bx, by, prizeX, prizeY);
            }
            return null;
        }

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day13)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            WriteLine(RealData);
        }

        public static void Test1()
        {
            var game = TestData1.First().ToGame();

            WriteLine(game);
        }
    }
}

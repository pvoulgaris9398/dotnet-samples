namespace Advent2024
{
    internal static class Day15
    {
        private record Point(int X, int Y)
        {
            public static Point operator +(Point p1, Point p2) => new(p1.X + p2.X, p1.Y + p2.Y);
        }

        private sealed record Robot(Point Current);

        private sealed record Box(Point Current)
        {
            public char Symbol = 'O';
        }

        private sealed record Wall(Point Point)
        {
            public char Symbol = '#';
        }

        private sealed record Empty(Point Point)
        {
            public char Symbol = '.';
        }

        private sealed record Left() : Point(0, -1);

        private sealed record Right() : Point(0, +1);

        private sealed record Up() : Point(-1, 0);

        private sealed record Down() : Point(+1, 0);

        private static List<string> RealData =>
            CommonFuncs.LoadFileData("..\\..\\..\\..\\..\\data\\day15.txt");

        private static char[][] TestMap1 =>
            [
                "########".ToCharArray(),
                "#..O.O.#".ToCharArray(),
                "##@.O..#".ToCharArray(),
                "#...O..#".ToCharArray(),
                "#.#.O..#".ToCharArray(),
                "#...O..#".ToCharArray(),
                "#......#".ToCharArray(),
                "########".ToCharArray(),
            ];

        private static void PrintArray(char[][] map)
        {
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    Write(map[i][j]);
                }
                Write("\n");
            }
        }

        private static char[] TestMovesData1 => "<^^>>>vv<v>>v<<".ToCharArray();

        private static IEnumerable<Point> TestMoves1 =>
            TestMovesData1.Select<char, Point>(c =>
                c switch
                {
                    '<' => new Left(),
                    '>' => new Right(),
                    '^' => new Up(),
                    'v' => new Down(),
                    _ => throw new NotImplementedException(),
                }
            );

        private static Point Move(this Point current, Point direction, char[][] map)
        {
            var nextMove = current + direction;
            var stack = new Stack<Point>();
            stack.Push(nextMove);

            while (stack.Count > 0)
            {
                if (nextMove.IsAtWall(map))
                    break;

                if (nextMove.IsAllowed(map))
                {
                    var first = map[current.X][current.Y];
                    var second = map[nextMove.X][nextMove.Y];

                    /* Swap them*/
                    map[current.X][current.Y] = second;
                    map[nextMove.X][nextMove.Y] = first;

                    // Walk back up the stack
                    nextMove = stack.Pop();
                }
                else
                {
                    current = nextMove;
                    nextMove += direction;
                    stack.Push(nextMove);
                }
                if (stack.Count > 0)
                {
                    continue;
                }
                else
                {
                    return nextMove;
                }
            }
            return current;
        }

        private static Point Move2(this Point current, Point direction, char[][] map)
        {
            var nextMove = current + direction;

            if (nextMove.IsAtWall(map))
            {
                return current;
            }

            if (nextMove.IsAllowed(map))
            {
                var first = map[current.X][current.Y];
                var second = map[nextMove.X][nextMove.Y];

                /* Swap them*/
                map[current.X][current.Y] = second;
                map[nextMove.X][nextMove.Y] = first;

                return nextMove;
            }
            else
            {
                return nextMove.Move(direction, map);
            }
        }

        private static bool IsAllowed(this Point current, char[][] map) =>
            map[current.X][current.Y] == '.';

        private static bool IsAtWall(this Point current, char[][] map) =>
            map[current.X][current.Y] == '#';

        private static Point Start(char[][] map)
        {
            for (var i = 0; i < map.Length; i++)
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '@')
                    return new Point(i, j);
            }
            throw new NotImplementedException();
        }

        private static void DoMoves(char[][] map, IEnumerable<Point> directions)
        {
            Point location = Start(map);
            WriteLine("Initial State");
            PrintArray(map);
            foreach (var direction in directions)
            {
                WriteLine($"Move {direction}:");
                location = location.Move(direction, map);
                PrintArray(map);
            }
        }

        public static void Run(bool testing = false)
        {
            WriteLine(new string('*', 80));
            WriteLine($"{nameof(Day15)}.{nameof(Run)}");
            WriteLine(new string('*', 80));

            Test1();
        }

        public static void Test1()
        {
            WriteLine(nameof(Test1));

            DoMoves(TestMap1, TestMoves1);
        }
    }
}

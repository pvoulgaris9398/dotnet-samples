﻿namespace Advent2024
{
    internal static class Day17
    {
        private const string Prod = "..\\..\\..\\..\\..\\data\\day17.txt";

        private const string Test1 = "..\\..\\..\\..\\..\\data\\day17-test.txt";

        internal static void Run(bool testing = false)
        {
            var path = testing ? Test1 : Prod;
            var machine = File.OpenText(path).ReadMachine();
            var output = string.Join(",", machine.Run());
            long seed = machine.FindSelfReplicatingSeeds(0).Min();

            WriteLine();
            machine.PrintProgram();
            WriteLine();

            WriteLine($"{nameof(output)}: {output}");
            WriteLine($"{nameof(seed)}: {seed}");
        }

        private static IEnumerable<long> FindSelfReplicatingSeeds(this Machine machine, int offset) =>
            machine.GetHigherSeeds(offset)
            .SelectMany(ExtendSeed)
            .Where(seed => machine.Run(seed).SequenceEqual(machine.Memory[offset..]));

        private static IEnumerable<byte> Run(this Machine machine, long seed) =>
            (machine with { A = seed }).Run();

        private static IEnumerable<long> ExtendSeed(long seed) =>
            Enumerable.Range(0, 8).Select(lower => (seed << 3) | (long)lower);

        private static long[] GetHigherSeeds(this Machine machine, int offset) =>
            offset == machine.Memory.Length - 1 ? [0]
            : [.. machine.FindSelfReplicatingSeeds(offset + 1)];

        private static void PrintProgram(this Machine machine) =>
            Enumerable.Range(0, machine.Memory.Length / 2)
            .Select(i => machine with { IP = 2 * i })
            .Select(ToPrintableInstruction)
            .ToList()
            .ForEach(WriteLine);

        private static string ToPrintableInstruction(this Machine machine) =>
            (machine.Memory[machine.IP], machine.Memory[machine.IP + 1]) switch
            {
                (0, byte op) => $"A <- A >> {op.ToPrintable()}",
                (1, byte op) => $"B <- B XOR {op}",
                (2, byte op) => $"B <- {op.ToPrintable()} AND 7",
                (3, byte op) => $"IP <- {op} (IF A = 0)",
                (4, _) => $"B <- B XOR C",
                (5, byte op) => $"OUT {op.ToPrintable()} AND 7",
                (6, byte op) => $"B <- A >> {op.ToPrintable()}",
                (7, byte op) => $"C <- A >> {op.ToPrintable()}",
                _ => throw new InvalidOperationException("Invalid instruction")
            };

        private static string ToPrintable(this byte combo) =>
            combo switch
            {
                <= 3 => $"{combo}",
                4 => "A",
                5 => "B",
                _ => "C"
            };

        private static IEnumerable<byte> Run(this Machine machine)
        {
            while (machine.IP < machine.Memory.Length)
            {
                (var output, machine) = machine.Step();
                if (output.HasValue) yield return output.Value;
            }
        }

        private static (byte? output, Machine machine) Step(this Machine m) =>
            m.FetchInstruction() switch
            {
                (0, long op) => (null, m with { A = m.A >> (int)op, IP = m.IP + 2 }),
                (1, long op) => (null, m with { B = m.B ^ op, IP = m.IP + 2 }),
                (2, long op) => (null, m with { B = op & 0x7, IP = m.IP + 2 }),
                (3, long op) => (null, m with { IP = m.A == 0 ? m.IP + 2 : op }),
                (4, _) => (null, m with { B = m.B ^ m.C, IP = m.IP + 2 }),
                (5, long op) => ((byte)(op & 0x7), m with { IP = m.IP + 2 }),
                (6, long op) => (null, m with { B = m.B >> (int)op, IP = m.IP + 2 }),
                (7, long op) => (null, m with { C = m.C >> (int)op, IP = m.IP + 2 }),
                _ => throw new InvalidOperationException()
            };

        private static (int opCode, long operand) FetchInstruction(this Machine machine) =>
            (machine.GetOpCode(), machine.GetOperand());

        private static int GetOpCode(this Machine machine) =>
            machine.Memory[machine.IP];

        private static long GetOperand(this Machine machine) =>
            (machine.GetOpCode(), machine.Memory[machine.IP + 1]) switch
            {
                (1, byte operand) => operand,
                (3, byte operand) => operand,
                (4, _) => 0,
                (_, byte operand) when operand <= 3 => operand,
                (_, 4) => machine.A,
                (_, 5) => machine.B,
                (_, 6) => machine.C,
                _ => throw new InvalidOperationException("Invalid operand")
            };

        private static IEnumerable<(string field, int[] values)> ParseInput(this TextReader text) =>
            text.ReadLines()
            .Select(line => line.Split(": "))
            .Where(parts => parts.Length == 2)
            .Select(parts => (
            field: parts[0],
            values: parts[1].Split(',').Select(int.Parse).ToArray()));

        private static Machine ReadMachine(this TextReader text) =>
            text.ParseInput().Aggregate
            (
                new Machine(0, 0, 0, 0, []),
                (machine, fields) => fields.field switch
                {
                    "Register A" => machine with { A = fields.values[0] },
                    "Register B" => machine with { B = fields.values[0] },
                    "Register C" => machine with { C = fields.values[0] },
                    _ => machine with { Memory = [.. fields.values.Select(v => (byte)v)] },
                }
            );

        private sealed record Machine(long A, long B, long C, long IP, byte[] Memory);
    }
}


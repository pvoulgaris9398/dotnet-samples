using Common;
namespace Algorithms
{
    /// <summary>
    /// Sunday, 4/6/25 WIP
    /// Advent of Code: Day3
    /// </summary>
    internal static partial class DayThree
    {
        internal static void Test0()
        {
            var data = LoadFileData("..\\..\\..\\..\\..\\day3.txt");

            var pairs = data[0].ToCharArray()
                /* I've got this down to just valid _transitions_
                 * Now I need to Zip??? them into a string that I can evaluate?
                 */
                .ToPairs()
                .Where(IsValid);

            foreach (var (prev, next) in pairs)
            {
                Console.WriteLine($"Previous: {prev}\tNext: {next}");
            }
        }

        internal static void Run()
        {
            /* Valid Sequences (where prev and next are):
            * "(" => Digit
            * Digit => Digit
            *  Digit => Decimal
            *  Decimal => Digit             
            * Digit => Comma
            * Comma => Digit
            * Digit => Digit
            *  Digit => Decimal
            *  Decimal => Digit 
            * Digit => ")"
            * 
            */

            /*  Valid multiplication sequences (or patterns)
             *  are of the form:
             *      open parenthesis
             *      number
             *      comma
             *      close parenthesis
             *  From my reading any spaces or other characters
             *  make these patterns invalid
             */
            Console.WriteLine(new string('*', 80));
            Console.WriteLine(nameof(Run));
        }


#pragma warning disable IDE0072 // Add missing cases
        public static bool IsValid(this (char previous, char current) pair) => pair switch
        {
            var (previous, current) when previous == '(' && char.IsDigit(current) => true,
            var (previous, current) when char.IsDigit(previous) && current == ',' => true,
            var (previous, current) when previous == ',' && char.IsDigit(current) => true,
            var (previous, current) when char.IsDigit(previous) && char.IsDigit(current) => true,
            var (previous, current) when char.IsDigit(previous) && current == ')' => true,
            var (_, _) => false
        };
#pragma warning restore IDE0072 // Add missing cases

        //public static bool IsValid(this (char previous, char current) pair)
        //{
        //    return pair switch
        //    {
        //        ('(', '1') => true,
        //        ('(', '2') => true,
        //        ('(', '3') => true,
        //        ('(', '4') => true,
        //        ('(', '5') => true,
        //        ('(', '6') => true,
        //        ('(', '7') => true,
        //        ('(', '8') => true,
        //        ('(', '9') => true,

        //        (_, _) => false,
        //        _ => throw new NotImplementedException()
        //    };
        //}

        private static List<string> LoadFileData(string path) => [.. File.OpenText(path).ReadLines()];
    }
}

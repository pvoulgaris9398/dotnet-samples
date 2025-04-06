namespace Algorithms
{
    internal static class ExampleOne
    {
        internal static void Run()
        {
            var path = "..\\..\\..\\..\\..\\data.txt";
            using TextReader reader = File.OpenText(path);
            (List<int> left, List<int> right) = LoadLists(reader);

            var totalDistance = left.Order()
                .Zip(right.Order(), (x, y) => Math.Abs(x - y))
                .Sum();

            /*
             * Very, very slick here!!!
             * Using the [???name???] property of multiplication
             * to convert multiplication into addition
             * if (3) occurs (5) times in the right list,
             * this returns (3) (5) times, then takes that sum
             * (5) + (5) + (5) +(5) + (5) = (15)
             * Instead of _counting_ (5) occurences of (3) in the right list
             * this code just returns (3) (5) times and takes the sum
             * in effect, the same thing but more elegant
             */
            int similarityScore = left
                .Join(right, l => l, r => r, (l, r) => r)
                .Sum();

            Console.WriteLine($"Total Items: {left.Count}");
            Console.WriteLine($"Total Distance: {totalDistance}");
            Console.WriteLine($"Similarity Scores: {similarityScore}");

        }

        private static (List<int> left, List<int> right) LoadLists(this TextReader text) =>
            text.ReadLines().Select(Common.ParseIntsNoSign).Transpose().ToPair();



    }
}
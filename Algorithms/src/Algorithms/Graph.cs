namespace Algorithms
{
    internal class Graph
    {
        public record Edge(int From, int To, int Weight);
        public static void Run()
        {
            List<Edge> edges = [];
            // Add some edges to the graph
            edges.Add(new(1, 2, 12));
            edges.Add(new(1, 3, 6));
            edges.Add(new(1, 4, 45));
            edges.Add(new(1, 5, 7));
            edges.Add(new(2, 1, 26));
            edges.Add(new(2, 4, 9));
            edges.Add(new(3, 2, 2));
            edges.Add(new(4, 3, 8));
            edges.Add(new(5, 2, 21));

            foreach (var item in Search(edges, 1))
            {
                WriteLine(item);
            }
        }

        public static IEnumerable<Edge> GetNeighbors(List<Edge> edges, int key) => edges
            .Where(e => e.From == key);

        /// <summary>
        /// Breadth-First Search
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static IEnumerable<(int, int)> Search(List<Edge> edges, int start)
        {
            // We might choose to carry along some information here
            // rather than just List if might be a custom object with
            // with additional state
            var visited = new Dictionary<int, int> { [start] = 0 };

            Queue<int> queue = new();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                yield return (current, visited[current]);

                foreach (var neighbor in GetNeighbors(edges, current))
                {
                    if (visited.TryGetValue(neighbor.To, out var found)
                        && found < current)
                    {
                        visited[neighbor.From] = found;
                    }
                    else
                    {
                        // Do something with them
                        visited[neighbor.From] = found;
                        queue.Enqueue(neighbor.To);
                    }
                }
            }
        }
    }
}
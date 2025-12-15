namespace Algorithms;

internal static class EdgeExtensions
{
    public static IEnumerable<Vertex> GetNeighbors(this Vertex vertex, List<Edge> edges) => edges
        .Where(e => e.From == vertex)
        .Select(e => e.To);

    public static void Traverse(this Vertex vertex, List<Edge> edges, Action<Vertex> process)
    {
        var visited = new HashSet<Vertex>() { vertex };

        Queue<Vertex> queue = new();

        queue.Enqueue(vertex);

        while (queue.Count > 0)
        {
            Vertex current = queue.Dequeue();

            // Handle neighbors
            foreach (Vertex neighbor in current.GetNeighbors(edges))
            {
                if (visited.Add(neighbor))
                {
                    queue.Enqueue(neighbor);
                }
            }

            // "process" current vertex
            process(current);
        }
    }
}

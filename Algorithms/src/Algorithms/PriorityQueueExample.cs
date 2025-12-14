using Distance = int;
namespace Algorithms;

internal static class PriorityQueueExample
{
    internal static void Run()
    {

        var sf = new City("San Francisco");
        var la = new City("Los Angeles");
        var dallas = new City("Dallas");
        var ny = new City("New York");
        var chicago = new City("Chicago:");

        sf.Edges = [
            new (la, 347),
            new(dallas, 1_480),
            new(chicago, 1_853),
            ];

        la.Edges = [
            new Edge(dallas, 1_237),
            new Edge(sf, 347),
        ];
        dallas.Edges = [
            new Edge(chicago, 802),
            new Edge(ny, 1_370),
            new Edge(sf, 1_480),
            new Edge(la, 1_237),
        ];
        chicago.Edges = [
            new Edge(ny, 712),
            new Edge(sf, 1_853),
            new Edge(dallas, 802),
        ];
        ny.Edges = [
            new Edge(dallas, 1_370),
            new Edge(chicago, 712),
        ];

        City[] allCities = [sf, la, dallas, ny, chicago];

        PrintShortestPath(allCities, sf, ny);

    }

    internal static List<(City, Distance)>? CalculateShortestPath(City[] cities,
        City startNode, City endNode)
    {

        var distances = cities
            .Select((city, i) => (city, details: (Previous: (City?)null, Distance: int.MaxValue)))
            .ToDictionary(x => x.city, x => x.details);

        var queue = new PriorityQueue<City, Distance>();

        distances[startNode] = (null, 0);

        queue.Enqueue(startNode, 0);

        while (queue.Count > 0)
        {
            City current = queue.Dequeue();

            if (current == endNode)
            {
                return BuildRoute(distances, endNode);
            }

            int currentNodeDistance = distances[current].Distance;

            foreach (Edge edge in current.Edges)
            {
                Distance distance = distances[edge.ConnectedTo].Distance;

                Distance newDistance = currentNodeDistance + edge.Distance;

                if (newDistance < distance)
                {

                    distances[edge.ConnectedTo] = (current, newDistance);

                    _ = queue.Remove(edge.ConnectedTo, out _, out _);

                    queue.Enqueue(edge.ConnectedTo, newDistance);
                }

            }

        }
        return null;
    }

    internal static List<(City, Distance)> BuildRoute(
    Dictionary<City, (City? previous, Distance Distance)> distances,
    City endNode)
    {
        var route = new List<(City, Distance)>();
        City? prev = endNode;

        while (prev is not null)
        {
            City current = prev;
            (prev, int distance) = distances[current];
            route.Add((current, distance));
        }
        route.Reverse();
        return route;
    }

    internal sealed record City(string Name)
    {
        public Edge[] Edges { get; set; } = [];
    }

    internal sealed record Edge(City ConnectedTo, Distance Distance);


    internal static void PrintShortestPath(City[] graph, City startNode, City endNode)
    {
        List<(City, int)>? route = CalculateShortestPath(graph, startNode, endNode);

        if (route is null)
        {
            WriteLine($"No route could be found between {startNode.Name} and {endNode.Name}");
            return;
        }
        WriteLine($"Shorted route between {startNode.Name} and {endNode.Name}");
        foreach ((City? node, int distance) in route)
        {
            WriteLine($"{node.Name}: {distance}");
        }
    }

}
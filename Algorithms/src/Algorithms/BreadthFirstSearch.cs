#!/usr/bin/env -S dotnet run

using System;
using System.Collections.Generic;

/*
Version using explicit Node object
which can contain additional information
*/

Graph graph = new(12);

graph.AddEdge(0, 1);
graph.AddEdge(0, 2);
graph.AddEdge(0, 3);
graph.AddEdge(1, 6);
graph.AddEdge(1, 7);
graph.AddEdge(2, 8);
graph.AddEdge(2, 9);
graph.AddEdge(8, 10);
graph.AddEdge(8, 11);
graph.AddEdge(3, 5);
graph.AddEdge(3, 4);

Console.WriteLine("Breadth First Traversal");

graph.Search(0, node => Console.Write($"{node} "));

public static class GraphExtensions
{
    /*
    Abstract the logic of how to fetch "neighbors"
    */
    public static IEnumerable<Node> NeighborsOf(this Graph graph, Node currentNode)
    {
        return graph.AdjacentNodes[currentNode];
    }

    public static void Search(this Graph graph, Node startNode, Action<Node> process)
    {
        HashSet<Node> visited = [startNode];
        Queue<Node> queue = new Queue<Node>();

        queue.Enqueue(startNode);

        while (queue.Count > 0)
        {
            Node currentNode = queue.Dequeue();

            process(currentNode);

            /*
             TODO: Come up with a strategy for ordering the nodes
             so we process them in a consistent order.
             Perhaps this can be passed-in from the outside
             (like the "process" callback) to allow customization
             based on some rule(s)?
            */
            foreach (Node neighbor in graph.NeighborsOf(currentNode)
            /*

            C# records do not automatically implement IComparable
            or IComparable<T>

            Without implementing that interface, this is what I get:

            Unhandled exception. System.InvalidOperationException: Failed to compare two elements in the array.
            ---> System.ArgumentException: At least one object must implement IComparable.
            ...

            */
            )
            {
                if (visited.Add(neighbor))
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
        Console.WriteLine();
    }
}

public record Node(int Value) : IComparable<Node>
{
    public static implicit operator Node(int value)
    {
        return new Node(value);
    }

    public int CompareTo(Node? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}

public sealed class Graph
{
    private readonly Dictionary<Node, List<Node>> _adjacentNodes;

    public int NodeCount => _adjacentNodes.Count();

    public IDictionary<Node, List<Node>> AdjacentNodes => _adjacentNodes;

    public Graph(int numberOfVertices)
    {
        _adjacentNodes = new Dictionary<Node, List<Node>>(numberOfVertices);
        for (int i = 0; i < numberOfVertices; ++i)
        {
            _adjacentNodes[i] = new List<Node>();
        }
    }

    public void AddEdge(Node from, Node to)
    {
        _adjacentNodes[from].Add(to);
    }
}

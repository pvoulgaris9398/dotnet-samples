#!/usr/bin/env -S dotnet run

using System;
using System.Collections.Generic;

/*
Version using explicit Node object
which can contain additional information
*/

Graph graph = new(6);

graph.AddEdge(0, 1);
graph.AddEdge(0, 2);
graph.AddEdge(1, 2);
graph.AddEdge(1, 3);
graph.AddEdge(3, 4);
graph.AddEdge(2, 5);

Console.WriteLine("Depth First Traversal");

Console.WriteLine("Using Recursion:");
graph.RecursiveSearch(0, node => Console.Write($"{node} "));

Console.WriteLine("Using Iterative Approach:");
graph.IterativeSearch(0, node => Console.Write($"{node} "));

public static class GraphExtensions
{
    private static void Search(
        this Graph graph,
        Node node,
        HashSet<Node> visited,
        Action<Node> process
    )
    {
        visited.Add(node);
        process(node);

        foreach (Node neighbor in graph.AdjacentNodes[node])
        {
            if (visited.Add(neighbor))
            {
                graph.Search(neighbor, visited, process);
            }
        }
    }

    public static void RecursiveSearch(this Graph graph, Node startNode, Action<Node> process)
    {
        HashSet<Node> visited = [startNode];
        graph.Search(startNode, visited, process);
        Console.WriteLine();
    }

    /*
    Abstract the logic of how to fetch "neighbors"
    */
    public static IEnumerable<Node> NeighborsOf(this Graph graph, Node currentNode)
    {
        return graph.AdjacentNodes[currentNode];
    }

    public static void IterativeSearch(this Graph graph, Node startNode, Action<Node> process)
    {
        HashSet<Node> visited = [startNode];
        Stack<Node> stack = new Stack<Node>();

        stack.Push(startNode);

        while (stack.Count > 0)
        {
            Node currentNode = stack.Pop();

            process(currentNode);

            /*
             TODO: Come up with a strategy for ordering the nodes
             so we process them in a consistent order.
             Perhaps this can be passed-in from the outside
             (like the "process" callback) to allow customization
             based on some rule(s)?
            */
            foreach (
                Node neighbor in graph
                    .NeighborsOf(currentNode)
                    /*
    
                    C# records do not automatically implement IComparable
                    or IComparable<T>
    
                    Without implementing that interface, this is what I get:
    
                    Unhandled exception. System.InvalidOperationException: Failed to compare two elements in the array.
                    ---> System.ArgumentException: At least one object must implement IComparable.
                    ...
    
                    */
                    .OrderBy(i => i)
                    .Reverse()
            )
            {
                if (visited.Add(neighbor))
                {
                    stack.Push(neighbor);
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

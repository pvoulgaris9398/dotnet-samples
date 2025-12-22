#!/usr/bin/env -S dotnet run

using System;
using System.Collections.Generic;

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
    private static void Search(this Graph graph, int node, bool[] visited, Action<int> process)
    {
        visited[node] = true;
        process(node);

        foreach (int neighbor in graph.AdjacentNodes[node])
        {
            if (!visited[neighbor])
            {
                graph.Search(neighbor, visited, process);
            }
        }
    }

    public static void RecursiveSearch(this Graph graph, int startNode, Action<int> process)
    {
        bool[] visited = new bool[graph.NodeCount];
        graph.Search(startNode, visited, process);
        Console.WriteLine();
    }

    public static void IterativeSearch(this Graph graph, int startNode, Action<int> process)
    {
        HashSet<int> visited = [startNode];
        Stack<int> stack = new Stack<int>();

        stack.Push(startNode);

        while (stack.Count > 0)
        {
            int current = stack.Pop();

            process(current);

            /*
             TODO: Come up with a strategy for ordering the nodes
             so we process them in a consistent order.
             Perhaps this can be passed-in from the outside
             (like the "process" callback) to allow customization
             based on some rule(s)?
            */
            foreach (int neighbor in graph.AdjacentNodes[current].OrderBy(i => i).Reverse())
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

public sealed class Graph
{
    private readonly List<int>[] _adjacencyList;

    public int NodeCount => _adjacencyList.Count();

    public IEnumerable<int>[] AdjacentNodes => _adjacencyList;

    public Graph(int numberOfVertices)
    {
        _adjacencyList = new List<int>[numberOfVertices];
        for (int i = 0; i < numberOfVertices; ++i)
        {
            _adjacencyList[i] = new List<int>();
        }
    }

    public void AddEdge(int from, int to)
    {
        _adjacencyList[from].Add(to);
    }
}

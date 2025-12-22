#!/usr/bin/env -S dotnet run

using System.Collections;
using UnionFindV2;

Console.WriteLine($"Running {nameof(Tests.Test0)}");

Tests.Test0();

Console.WriteLine("Complete\n");

internal static class Tests
{
    internal static void Test0()
    {
        DisjointSet<int> set = new DisjointSet<int>();
        int[] values = [10, 20, 30, 40, 50, 60];
        foreach (var value in values)
        {
            set.MakeSet(value);
        }
    }
}

namespace UnionFindV2
{
    public class Node<T>
        where T : System.IComparable<T>
    {
        public T Data { get; set; }
        public Node<T> Parent { get; set; }
        public int Rank { get; set; }

        public Node(T data)
        {
            Data = data;
            Parent = this;
            Rank = 0;
        }
    }

    public class DisjointSet<T> : IEnumerable<T>
        where T : System.IComparable<T>
    {
        private readonly Dictionary<T, Node<T>> _nodes;
        public int Count => _nodes.Count;

        public DisjointSet()
        {
            _nodes = new Dictionary<T, Node<T>>();
        }

        public IEnumerator<T> GetEnumerator() => _nodes.Keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool ContainsData(T data) => _nodes.ContainsKey(data);

        public bool MakeSet(T data)
        {
            if (ContainsData(data))
            {
                return false;
            }
            _nodes.Add(data, new Node<T>(data));
            return true;
        }

        public bool Union(T a, T b)
        {
            var nodeA = _nodes[a];
            var nodeB = _nodes[b];

            var parentA = nodeA.Parent;
            var parentB = nodeB.Parent;

            if (parentA == parentB)
            {
                return false;
            }

            if (parentA.Rank >= parentB.Rank)
            {
                if (parentA.Rank == parentB.Rank)
                {
                    ++parentA.Rank;
                }
                parentB.Parent = parentA;
            }
            else
            {
                parentA.Parent = parentB;
            }
            return true;
        }

        public T FindSet(T data)
        {
            return DisjointSet<T>.FindSet(_nodes[data]).Data;
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void Clear()
        {
            _nodes.Clear();
        }

        private static Node<T> FindSet(Node<T> node)
        {
            var parent = node.Parent;
            if (parent == node)
            {
                return node;
            }
            node.Parent = DisjointSet<T>.FindSet(node.Parent);
            return node.Parent;
        }
    }
}

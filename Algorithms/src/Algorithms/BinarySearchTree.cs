#!/usr/bin/env -S dotnet run

int[] values = [30, 8, 20, 12, 22];

Node? root = null;

foreach (var value in values)
{
    root = Functions.Insert(root, value);
}

Console.WriteLine(root);

public class Functions
{
    internal static Node Insert(Node? root, int value)
    {
        if (root is null)
        {
            return new Node(value, null, null);
        }

        if (value < root.Value)
        {
            Insert(root.Left, value);
        }
        else
        {
            Insert(root.Right, value);
        }

        return root;
    }
}

public sealed record Node(int Value, Node? Left = null, Node? Right = null);

namespace Algorithms;

internal sealed class Node<T>(T value, Node<T> left, Node<T> right) where T : IComparable<T>
{
    public T Value { get; } = value;
    public Node<T>? Left { get; } = left;
    public Node<T>? Right { get; } = right;
}

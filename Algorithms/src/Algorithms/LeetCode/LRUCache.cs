#!/usr/bin/env -S dotnet run

var cache = new LRUCache(2);
Console.WriteLine("null");
cache.Put(1, 1);
Console.WriteLine("null");
cache.Put(2, 2);
Console.WriteLine("null");
Console.WriteLine(cache.Get(1));
cache.Put(3, 3);
Console.WriteLine("null");
Console.WriteLine(cache.Get(2));
cache.Put(4, 4);
Console.WriteLine("null");
Console.WriteLine(cache.Get(1));
Console.WriteLine(cache.Get(3));
Console.WriteLine(cache.Get(4));

/*
https://leetcode.com/problems/lru-cache/description/
*/
public class LRUCache
{
    Dictionary<int, int> _cache;
    LinkedList<int> _leastRecentlyUsed;
    private int _capacity;

    public LRUCache(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1, nameof(capacity));
        _capacity = capacity;
        _cache = new(_capacity);
        _leastRecentlyUsed = new();
    }

    public int Get(int key)
    {
        if (_cache.TryGetValue(key, out int value))
        {
            _leastRecentlyUsed.Remove(key);
            _leastRecentlyUsed.AddFirst(key);
            return value;
        }
        return -1;
    }

    public void Put(int key, int value)
    {
        if (_cache.ContainsKey(key))
        {
            _cache[key] = value;
            _leastRecentlyUsed.Remove(key);
            _leastRecentlyUsed.AddFirst(key);
        }
        else
        {
            if (_cache.Count == _capacity)
            {
                var last = _leastRecentlyUsed.Last();
                _leastRecentlyUsed.RemoveLast();
                _cache.Remove(last);
            }

            _cache.Add(key, value);
            _leastRecentlyUsed.AddFirst(key);
        }
    }
}

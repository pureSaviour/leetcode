LRUCache cache = new(2);
cache.Put(1, 1);
cache.Put(2, 2);
Console.WriteLine(cache.Get(1));
cache.Put(3, 3);
Console.WriteLine(cache.Get(2));
cache.Put(4, 4);
Console.WriteLine(cache.Get(1));
Console.WriteLine(cache.Get(3));
Console.WriteLine(cache.Get(4));

public class LRUCache(int capacity)
{
    private readonly Dictionary<int, LinkedListNode<(int key, int value)>> _cache = new(capacity);
    private readonly int _capacity = capacity;
    private readonly LinkedList<(int key, int value)> _lruList = [];

    public int Get(int key) {
        if (!_cache.TryGetValue(key, out var node)) return -1;
        
        _lruList.Remove(node);
        _lruList.AddFirst(node);
        return node.Value.value;
    }
    
    public void Put(int key, int value) {
        if (_cache.TryGetValue(key, out var value1))
        {
            _lruList.Remove(value1);
            _lruList.AddFirst(value1);
            value1.Value = (key, value);
            return;
        }

        var node = new LinkedListNode<(int key, int value)>((key, value));
        if (_cache.Count >= _capacity)
        {
            var last = _lruList.Last;
            if (last is not null)
            {
                _cache.Remove(last.Value.key);
                _lruList.RemoveLast();
            }
        }
        _cache.Add(key, node);
        _lruList.AddFirst(node);
    }
}
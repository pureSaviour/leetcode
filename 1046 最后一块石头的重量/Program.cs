Solution sol = new Solution();
int[] stones = [2, 7, 4, 1, 8, 1];
var res = sol.LastStoneWeight(stones);
Console.WriteLine(res);


public class Solution
{
    private Heap? _heap;
    public int LastStoneWeight(int[] stones) {
        _heap = new Heap(stones);
        while (_heap.Count > 1)
        {
            var first = _heap.Dequeue();
            var second = _heap.Dequeue();
            first -= second;
            if(first != 0)
                _heap.Enqueue(first);
        }
        return _heap.Count == 0 ? 0 : _heap.Dequeue();
    }
}

public class Heap
{
    private int[] _array;
    private int _size;
    private int _capacity;

    public Heap():this(4){}

    public Heap(int capacity)
    {
        _array = new int[capacity];
        _size = 0;
        _capacity = capacity;
    }

    public Heap(ICollection<int> collection)
    {
        _array = new int[collection.Count];
        collection.CopyTo(_array, 0);
        _size = collection.Count;
        _capacity = collection.Count;
        Heapify();
    }
    public int Peek() => _size > 0 ? _array[0] : throw new InvalidOperationException();
    public int Count => _size;
    public int Capacity => _capacity;

    public void Enqueue(int item)
    {
        if (_size == _capacity)
        {
            var newCapacity = _capacity == 0 ? 4 : _capacity * 2;
            Array.Resize(ref _array, newCapacity);
            _capacity = newCapacity;
        }
        
        _array[_size] = item;
        _size++;
        AdjustUp(_size - 1);
    }
    public int Dequeue()
    {
        if(_size == 0) throw new InvalidOperationException();
        if(_size == 1) return _array[--_size];
        var res = _array[0];
        (_array[0], _array[_size - 1]) = (_array[_size - 1], _array[0]);
        --_size;
        AdjustDown(0);
        return res;
    }
    
    private int GetParentIndex(int index) => (index - 1) >> 1;
    private int GetFirstChildIndex(int index) => (index << 1) + 1;

    private void Heapify()
    {
        for (var i = GetParentIndex(_size - 1); i >= 0; i--)
        {
            AdjustDown(i);
        }
    }
    
    private void AdjustUp(int index)
    {
        ThrowIfIndexOutOfRange(index);
        
        var parentIndex = GetParentIndex(index);
        while (parentIndex >= 0 && _array[index] > _array[parentIndex])
        {
            (_array[index],_array[parentIndex]) = (_array[parentIndex], _array[index]);
            index = parentIndex;
            parentIndex = GetParentIndex(index);
        }
    }

    private void AdjustDown(int index)
    {
        ThrowIfIndexOutOfRange(index);
        
        var left = GetFirstChildIndex(index);
        while (left < _size)
        {
            var maxIndex = (left + 1 < _size && _array[left] < _array[left + 1]) ? left + 1 : left;
            if (_array[index] < _array[maxIndex])
            {
                (_array[index],_array[maxIndex]) = (_array[maxIndex], _array[index]);
                index = maxIndex;
                left = GetFirstChildIndex(index);
            }
            else
            {
                break;
            }
        }
    }
    
    private void ThrowIfIndexOutOfRange(int index)
    {
        if(index < 0 || index >= _size) throw new IndexOutOfRangeException();
    }
}
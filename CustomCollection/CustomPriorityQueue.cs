using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CustomCollection;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CustomPriorityQueueView<,>))]
public class CustomPriorityQueue<TElement, TPriority>
{
#nullable disable
    
    private (TElement Element, TPriority Priority)[] _nodes;
    private readonly IComparer<TPriority> _comparer;
    private int _size;
    private int _version;
    
#nullable  enable
    
    public CustomPriorityQueue() : this(0){}

    public CustomPriorityQueue(IComparer<TPriority> comparer):this(0, comparer){}
    
    public CustomPriorityQueue(int capacity, IComparer<TPriority>? comparer = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));
        _nodes = new (TElement, TPriority)[capacity];
        _size = 0;
        _comparer = InitComparer(comparer);
    }

    public CustomPriorityQueue(IEnumerable<(TElement, TPriority)> items, IComparer<TPriority>? comparer = null)
    {
        ArgumentNullException.ThrowIfNull(items);
        _nodes = CustomEnumerableHelper.ToArray(items, out _size);
        _comparer = InitComparer(comparer);
        Heapify();
    }

    public int Count => _size;
    public int Capacity => _nodes.Length;
    public IComparer<TPriority> Comparer => _comparer;

    public void Enqueue(TElement element, TPriority priority)
    {
        ++_version;
        if(_size == Capacity)
            Resize(_size + 1);
        ++_size;
        AdjustUp((element, priority), _size - 1);
    }

    public TElement Peek()
    {
        if(_size == 0)
            throw new InvalidOperationException("The queue is empty.");
        return _nodes[0].Element;
    }

    public TElement Dequeue()
    {
        if(_size == 0)
            throw new InvalidOperationException("The queue is empty.");
        var rootEle = _nodes[0].Element;
        RemoveRoot();
        
        return rootEle;
    }

    public bool TryDequeue([MaybeNullWhen(false)]out TElement element,[MaybeNullWhen(false)] out TPriority priority)
    {
        if (_size == 0)
        {
            element = default;
            priority = default;
            return false;
        }
        (element, priority) = _nodes[0];
        RemoveRoot();
        return true;
    }

    public bool TryPeek([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority)
    {
        if (_size == 0)
        {
            element = default;
            priority = default;
            return false;
        }
        (element, priority) = _nodes[0];
        return true;
    }

    public void EnqueueRange(IEnumerable<(TElement Element, TPriority Priority)> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        var startIndex = _size;
        var newItems = CustomEnumerableHelper.ToArray(items, out var newItemsCount);
        if (newItemsCount == 0) return;
        var endIndex = startIndex + newItemsCount;
        if (Capacity < _size + newItemsCount)
        {
            Resize(newItemsCount + _size);
        }
        Array.Copy(newItems, 0, _nodes, startIndex, newItemsCount);
        _size += newItemsCount;
        for (int i = startIndex; i < endIndex; ++i)
        {
            AdjustUp(_nodes[i], i);
        }
    }
    
    private void Resize(int minCapacity)
    {
        var capacity = _nodes.Length;
        int newCapacity = capacity == 0 ? 4
            : capacity * 2 < minCapacity ? minCapacity
            : capacity * 2;
        newCapacity = Math.Max(minCapacity, newCapacity);
        Array.Resize(ref _nodes, newCapacity);
    }

    private void RemoveRoot()
    {
        ++_version;
        var index = --_size;
        if(index <= 0) return;
        var node = _nodes[index];
        AdjustDown(node, 0);
        this._nodes[index] = default;
    }

    private void Heapify()
    {
        (TElement Element, TPriority Priority)[] nodes = _nodes;
        var parentIndex = GetParentIndex(_size - 1);
        for (var i = parentIndex; i >= 0; --i)
        {
            AdjustDown(nodes[i], i);
        }
    }

    private void AdjustUp((TElement Element, TPriority Priority) node,int index)
    {
        var comparer = _comparer;
        var nodes = _nodes;
        var parentIndex = GetParentIndex(index);
        while (parentIndex >= 0)
        {
            var parent = nodes[parentIndex];
            if (comparer.Compare(node.Priority, parent.Priority) < 0)
            {
                nodes[index] = parent;
                index = parentIndex;
                parentIndex = GetParentIndex(index);
            }else break;
        }
        nodes[index] = node;
    }
    private void AdjustDown((TElement Element, TPriority Priority) node,int index)
    {
        var comparer = _comparer;
        var nodes = _nodes;
        var first = GetFirstChildIndex(index);
        while (first < _size)
        {
            var minIndex = Math.Min(_size, first + 4);
            var minValue = nodes[first];
            var targetIndex = first; 
            while (++first < minIndex)
            {
                var target = nodes[first];
                if (comparer.Compare(target.Priority, minValue.Priority) < 0)
                {
                    minValue = target;
                    targetIndex = first;
                }
            }

            if (comparer.Compare(minValue.Priority, node.Priority) < 0)
            {
                nodes[index] = minValue;
                index = targetIndex;
                first = GetFirstChildIndex(index);
            }else break;
        }
        nodes[index] = node;
    }
    
    private int GetParentIndex(int index) => (index - 1) >> 2;     
    private int GetFirstChildIndex(int index) => (index << 2) + 1;
    
    private static IComparer<TPriority> InitComparer(IComparer<TPriority>? comparer)
    {
        return Comparer<TPriority>.Default;
        if(!typeof(TPriority).IsValueType)
            return comparer ?? (IComparer<TPriority>) System.Collections.Generic.Comparer<TPriority>.Default;
        return comparer == System.Collections.Generic.Comparer<TPriority>.Default ? (IComparer<TPriority>) null : comparer;
    }
    
    public sealed class UnorderedItemsCollection(CustomPriorityQueue<TElement, TPriority> nodes, CustomPriorityQueue<TElement, TPriority> queue) :
        IReadOnlyCollection<(TElement Element, TPriority Priority)>,
        ICollection
    {
        internal readonly CustomPriorityQueue<TElement, TPriority> _queue = queue;

        public void CopyTo(Array array, int index)
        {
            ArgumentNullException.ThrowIfNull(array);
            if(array.Rank != 1) 
                throw new ArgumentException("Only single dimensional arrays are supported.", nameof(array));
            if (array.GetLowerBound(0) != 0)
                throw new ArgumentException("The lower bound of the target array must be zero.", nameof(array));
            if (index < 0 || index > array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < nodes._size)
                
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from index to the end of the destination array.", nameof(array));
            Array.Copy(nodes._nodes, 0, array, index, _queue.Count);
        }

        int ICollection.Count => _queue.Count;

        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => this;

        int IReadOnlyCollection<(TElement Element, TPriority Priority)>.Count => _queue.Count;
        
        public struct Enumerator : 
            IEnumerator<(TElement Element, TPriority Priority)>
        {
            private (TElement Element, TPriority Priority) _current;
            private readonly CustomPriorityQueue<TElement, TPriority> _queue;
            private readonly int _version;
            private int _index;

            public Enumerator(CustomPriorityQueue<TElement, TPriority> queue)
            {
                _queue = queue;
                _version = _queue._version;
            }

            public bool MoveNext()
            {
                var queue = _queue;
                if (queue._version != _version)
                    throw new InvalidOperationException("The collection is read-only.");
                if (_index < queue._size)
                {
                    _current = queue._nodes[_index];
                    _index++;
                    return true;
                }

                _current = default;
                _index = -1;
                return false;
            }

            void IEnumerator.Reset()
            {
                if (_version != _queue._version)
                    throw new InvalidOperationException("The collection is read-only.");
                _index = 0;
                _current = default;
            }

            (TElement Element, TPriority Priority) IEnumerator<(TElement Element, TPriority Priority)>.Current => _current;

            object IEnumerator.Current => _current;

            public void Dispose(){}
        }

        public IEnumerator<(TElement Element, TPriority Priority)> GetEnumerator()
        {
            return new Enumerator(_queue);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


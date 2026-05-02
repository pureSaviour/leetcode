using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CustomCollection;

[DebuggerTypeProxy(typeof(CustomICollectionDebugView<>))]
[DebuggerDisplay("Count = {Count}")]
public class CustomStack<T> : IReadOnlyCollection<T>
{
    private int _count;
    private int _version;
    private T[] _array;
    
    private const int DefaultCapacity = 4;

    public CustomStack():this(DefaultCapacity){}

    public CustomStack(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);
        _array = new T[capacity];
        _count = 0;
        _version = 0;
    }

    public void Clear()
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            Array.Clear(_array, 0, _count);    
        _count = 0;
        _version++;
    }

    public bool Contains(T item)
    {
        var c = EqualityComparer<T>.Default;
        for (int i = _count - 1; i >= 0; --i)
            if(c.Equals(_array[i], item)) return true;
        return false;
    }

    public void CopyTo(T[] array, int startIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        if(array.Rank != 1) throw new ArgumentException("Only single dimension array is supported.", nameof(array));
        if(startIndex < 0 || startIndex > array.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if(startIndex + _count >_array.Length) throw new ArgumentException("Not enough space in the target array.", nameof(array));
        Array.Copy(_array, 0, array, startIndex, Count);
        Array.Reverse(array, startIndex, Count);
    }

    public void TrimExcess()
    {
        int threshold = (int)(Capacity * 0.9);
        if (_count < threshold)
            Array.Resize(ref _array, threshold);
    }

    public void TrimExcess(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, _count);
        
        if(capacity == Capacity) return;
        Array.Resize(ref _array, capacity);
    }

    public T Peek()
    {
        if(_count == 0)
            throw new InvalidOperationException("Stack is empty.");
        return _array[_count - 1];
    }

    public bool TryPeek([MaybeNullWhen(false)] out T item)
    {
        if (_count == 0)
        {
            item = default;
            return false;
        }
        item = _array[_count - 1];
        return true;
    }
    
    
    public T Pop()
    {
        int index = _count - 1;
        if((uint)index >= (uint)Capacity)
            ThrowIfEmpty();

        var item = _array[index];
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            _array[index] = default!;
        _count = index;
        _version++;
        return item;
    }

    public bool TryPop([MaybeNullWhen(false)] out T item)
    {
        int index = _count - 1;
        if ((uint)index >= (uint)Capacity)
        {
            item = default;
            return false;
        }
        item = _array[index];
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            _array[index] = default!;
        _count = index;
        _version++;
        return true;
    }

    public void Push(T item)
    {
        if ((uint)_count >= (uint)Capacity)
            Grow(_count + 1);
        _array[_count++] = item;
        _version++;
    }
    
    public void EnsureCapacity(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 0);
        if (Capacity < capacity)
            Grow(capacity);
    }

    public T[] ToArray()
    {
        if(_count == 0) return [];
        var array = new T[_count];
        Array.Copy(_array, array, _count);
        Array.Reverse(array);
        return array;
    }
    
    private void Grow(int capacity)
    {
        Debug.Assert(Capacity < capacity);
        var newCapacity = Capacity == 0 ? DefaultCapacity : Capacity * 2;
        if((uint)newCapacity >= (uint)Array.MaxLength) newCapacity = Array.MaxLength;
        if (newCapacity < capacity) newCapacity = capacity;
        
        Array.Resize(ref _array, newCapacity);
    }

    private void ThrowIfEmpty()
    {
        Debug.Assert(_count == 0, "Only call this method when the stack is empty.");
        throw new InvalidOperationException("Stack is empty.");
    }

    public IEnumerator<T> GetEnumerator() => new CustomEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => _count;

    public int Capacity => _array.Length;

    public struct CustomEnumerator : IEnumerator<T>
    {
        private T? _current;
        private readonly CustomStack<T> _stack;
        private readonly int _version;
        private int _index;

        internal CustomEnumerator(CustomStack<T> stack)
        {
            _stack = stack;
            _version = stack._version;
            _index = stack.Count;
            _current = default;
        }
        public void Dispose(){}

        public bool MoveNext()
        {
            if(_version != _stack._version) throw new InvalidOperationException("Collection was modified after the enumerator was created.");

            var index = _index - 1;
            if ((uint)index >= (uint)_stack.Count)
            {
                _current = default;
                _index = -1;
                return false;
            }

            _current = _stack._array[index];
            _index = index;
            return true;
        }

        public void Reset()
        {
            if(_version != _stack._version) throw new InvalidOperationException("Collection was modified after the enumerator was created.");
            _current = default;
            _index = _stack.Count;
        }

        T IEnumerator<T>.Current => _current!;

        object? IEnumerator.Current => _current;
    }
}
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CustomCollection;

[DebuggerTypeProxy(typeof(CustomIProducerConsumerCollectionDebugView<>))]
[DebuggerDisplay("Count = {Count}")]
public sealed class CustomConcurrentStack<T> : IProducerConsumerCollection<T>, IReadOnlyCollection<T>
{
    private volatile Node? _head;
    private const int BackoffMaxYields = 64;
    public CustomConcurrentStack() { _head = null; }

    public CustomConcurrentStack(IEnumerable<T> collection)
    {
        var lastNode = _head;
        foreach (var item in collection)
        {
            var newNode = new Node(item) { Next = lastNode };
            lastNode = newNode;
        }

        _head = lastNode;
    }

    public void Clear() => _head = null;
    public void Push(T item)
    {
        Node newNode = new Node(item) { Next = _head };
        if(Interlocked.CompareExchange(ref _head, newNode, newNode.Next) == newNode.Next)
            return;

        PushCore(newNode, newNode);
    }
    
    public void PushRange(T[] items) => PushRange(items, 0, items.Length);
    public void PushRange(T[] items, int startIndex, int count)
    {
        ValidatePushPopRangeInput(items, startIndex, count);

        var endIndex = startIndex + count;
        Node head, tail;
        head = tail = new Node(items[startIndex]);
        for (int i = startIndex + 1; i < endIndex; ++i)
        {
            Node node = new Node(items[i]) { Next = head };
            head = node;
        }
        tail.Next = _head;

        if(Interlocked.CompareExchange(ref _head, head, tail.Next) == tail.Next)
            return;
        PushCore(head, tail);
    }

    public bool TryPeek([MaybeNullWhen(false)] out T item)
    {
        Node? head = _head;
        if (head is null)
        {
            item = default;
            return false;
        }
        item = head.Value;
        return true;
    }

    public bool TryPop([MaybeNullWhen(false)] out T item)
    {
        Node? head = _head;
        if (head is null)
        {
            item = default;
            return false;
        }

        if (Interlocked.CompareExchange(ref _head, head.Next, head) == head)
        {
            item = head.Value;
            return true;
        }

        if (TryPopCore(1, out var node) == 1 && node is not null)
        {
            item = node.Value;
            return true;
        }
        item = default;
        return false;
    }

    public int TryPopRange(T[] items) => TryPopRange(items, 0, items.Length);
    public int TryPopRange(T[] items, int startIndex, int count)
    {
        ValidatePushPopRangeInput(items, startIndex, count);
        int popCount = TryPopCore(count, out var head);
        if (popCount > 0)
            ToList(head).CopyTo(items, startIndex);
        return popCount;
    }

    private static void ValidatePushPopRangeInput(T[] items, int startIndex, int count)
    {
        ArgumentNullException.ThrowIfNull(items);
        if(startIndex < 0 || startIndex > items.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if(count < 0 || count > items.Length - startIndex) throw new ArgumentOutOfRangeException(nameof(count));
    }

    private void PushCore(Node head, Node tail)
    {
        SpinWait spinWait = default;
        do
        {
            spinWait.SpinOnce(sleep1Threshold: -1);     // No need to call Thread.Sleep(1) in this method, so we disable it by passing -1.
            tail.Next = _head;
        } while (Interlocked.CompareExchange(ref _head, head, tail.Next) != tail.Next);
    }

    private int TryPopCore(int count, out Node? node)
    {
        SpinWait spin = default;
        int backoff = 1;
        do
        {
            Node? head = _head;
            Node next;
            int nodesCount = 1;
            if (head is null)
            {
                node = null;
                return 0;
            }

            for (next = head; next.Next is not null && nodesCount < count; next = next.Next, ++nodesCount) ;
            if (Interlocked.CompareExchange(ref _head, next.Next, head) == head)
            {
                next.Next = null;
                node = head;
                return nodesCount;
            }

            for (int i = 0; i < backoff; ++i)
                spin.SpinOnce(-1);
            // 竞争严重，随机增加自旋次数
            if (spin.NextSpinWillYield)
                backoff = Random.Shared.Next(minValue: 1, maxValue: BackoffMaxYields);
            else backoff *= 2;
        } while (true);
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node? current = _head;
        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void CopyTo(T[] array, int index)
    {
        ArgumentNullException.ThrowIfNull(array);
        if(index < 0 || index > array.Length) throw new ArgumentOutOfRangeException(nameof(index));
        ((ICollection)ToList(_head)).CopyTo(array, index);
    }
    
    void ICollection.CopyTo(Array array, int index)
    {
        ArgumentNullException.ThrowIfNull(array);
        if(array.Rank != 1) throw new ArgumentException("The array is not multidimensional.", nameof(array));
        if(index < 0 || index > array.Length) throw new ArgumentOutOfRangeException(nameof(index));
        
        ((ICollection)ToList(_head)).CopyTo(array, index);
    }

    private static List<T> ToList(Node? node)
    {
        if (node is null) return [];
        var list = new List<T>();
        for (Node? current = node; current is not null; current = current.Next)
            list.Add(current.Value);
        return list;
    }
    public bool IsSynchronized => false;
    public object SyncRoot => throw new NotSupportedException("ConcurrentCollection does not support synchronization.");

    public T[] ToArray() => ToList(_head).ToArray();

    bool IProducerConsumerCollection<T>.TryAdd(T item)
    {
        Push(item);
        return true;
    }

    public bool TryTake([MaybeNullWhen(false)] out T item) => TryPop(out item);
    
    public int Count
    {
        get
        {
            int count = 0;
            for (Node? current = _head; current is not null; current = current.Next)
                ++count;
            return count;
        }
    }
    
    public bool IsEmpty => _head is null;
    
    private sealed class Node(T value)
    {
        internal readonly T Value = value;
        internal Node? Next;
    }
}
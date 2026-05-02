using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace CustomCollection;

public class CustomConcurrentStack<T> : IProducerConsumerCollection<T>, IReadOnlyCollection<T>
{
    private int _count;
    private Node? _head;

    public void Push(T item)
    {
        Node newNode = new Node(item);
        newNode._next = _head;
        if(Interlocked.CompareExchange(ref _head, newNode, newNode._next) == _head)
            return;

        PushCore(newNode, newNode);
    }

    public void PushRange(T[] items, int startIndex, int count)
    {
        ValidatePushPopRangeInput(items, startIndex, count);

        var endIndex = startIndex + count;
        Node head, tail;
        head = tail = new Node(items[startIndex]);
        for (int i = startIndex + 1; i < endIndex; ++i)
        {
            Node node = new Node(items[i]);
            node._next = head;
            head = node;
        }
        tail._next = head;

        if(Interlocked.CompareExchange(ref _head, head, tail._next) == tail._next)
            return;
        PushCore(head, tail);
    }

    private static void ValidatePushPopRangeInput(T[] items, int startIndex, int count)
    {
        ArgumentNullException.ThrowIfNull(items);
        if(startIndex < 0 || startIndex > items.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if(count < 0 || count > items.Length) throw new ArgumentOutOfRangeException(nameof(count));
        if (startIndex + count > items.Length) throw new ArgumentException("The sum of startIndex and count cannot be greater than the length of items.");
    }

    private void PushCore(Node head, Node tail)
    {
        SpinWait spinWait = default;
        do
        {
            spinWait.SpinOnce(sleep1Threshold: -1);     // No need to call Thread.Sleep(1) in this method, so we disable it by passing -1.
            tail._next = head;
        } while (Interlocked.CompareExchange(ref _head, head, tail._next) != tail._next);
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void CopyTo(Array array, int index)
    {
        throw new NotImplementedException();
    }

    int ICollection.Count => _count;

    public bool IsSynchronized { get; }
    public object SyncRoot { get; }
    public void CopyTo(T[] array, int index)
    {
        throw new NotImplementedException();
    }

    public T[] ToArray()
    {
        throw new NotImplementedException();
    }

    public bool TryAdd(T item)
    {
        throw new NotImplementedException();
    }

    public bool TryTake([MaybeNullWhen(false)] out T item)
    {
        throw new NotImplementedException();
    }

    public int Count => _count;
    
    private sealed class Node
    {
        private T _value;
        internal Node? _next;

        public Node(T value)
        {
            _value = value;
            _next = null;
        }
    }
}
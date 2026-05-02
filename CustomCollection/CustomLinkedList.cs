using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace CustomCollection;

[DebuggerTypeProxy(typeof(CustomICollectionDebugView<>))]
[DebuggerDisplay("Count = {Count}")]
public class CustomLinkedList<T>: 
    ICollection<T>,
    IReadOnlyCollection<T>
{
    // TODO: 实现一个双向链表，支持以下操作：
    // TODO: 1. 添加元素到链表末尾(AddLast)开头(AddFirst)
    // TODO: 2. 添加元素到链表指定位置(AddBefore, AddAfter)
    // TODO: 3. 删除链表中的元素(Remove, RemoveFirst, RemoveLast)
    // TODO: 4. 查找链表中的元素(Find, FindLast)
    
    private int _count;
    private int _version;
    private CustomLinkedListNode<T>? _head;

    public CustomLinkedList()
    {
        _count = 0;
        _version = 0;
        _head = null;
    }

    public CustomLinkedList(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        foreach (var item in collection)
            AddLast(item);
    }

    public CustomLinkedListNode<T> AddAfter(CustomLinkedListNode<T> node, T value)
    {
        ValidateNode(node);
        
        CustomLinkedListNode<T> newNode = new(this, value);
        InternalInsertNodeBefore(node.Next!, newNode);
        return newNode;
    }
    
    public void AddAfter(CustomLinkedListNode<T> node, CustomLinkedListNode<T> newNode)
    {
        ValidateNode(node);
        ValidateNewNode(newNode);
        
        InternalInsertNodeBefore(node.Next!, newNode);
        newNode.list = this;
    }

    public CustomLinkedListNode<T> AddBefore(CustomLinkedListNode<T> node, T value)
    {
        ValidateNode(node);
        
        CustomLinkedListNode<T> newNode = new(this, value);
        InternalInsertNodeBefore(node, newNode);
        if(node == _head)
            _head = newNode;
        return newNode;
    }
    
    public void AddBefore(CustomLinkedListNode<T> node, CustomLinkedListNode<T> newNode)
    {
        ValidateNode(node);
        ValidateNewNode(newNode);
        
        InternalInsertNodeBefore(node, newNode);
        if(node == _head)
            _head = newNode;
        newNode.list = this;
    }
    
    public CustomLinkedListNode<T> AddFirst(T value)
    {
        CustomLinkedListNode<T> node = new(this, value);
        if (_head is null)
        {
            InternalInsertNodeToEmptyList(node);
        }
        else
        {
            InternalInsertNodeBefore(_head, node);
            _head = node;
        }
        return node;
    }

    public void AddFirst(CustomLinkedListNode<T> node)
    {
        ValidateNewNode(node);
        if (_head is null)
            InternalInsertNodeToEmptyList(node);
        else
        {
            InternalInsertNodeBefore(_head, node);
            _head = node;
        }

        node.list = this;
    }

    public CustomLinkedListNode<T> AddLast(T value)
    {
        CustomLinkedListNode<T> node = new(this, value);
        if (_head is null)
            InternalInsertNodeToEmptyList(node);
        else
            InternalInsertNodeBefore(_head, node);
        return node;
    }

    public void AddLast(CustomLinkedListNode<T> node)
    {
        ValidateNewNode(node);
        if (_head is null)
            InternalInsertNodeToEmptyList(node);
        else
            InternalInsertNodeBefore(_head, node);
        node.list = this;
    }

    public void Clear()
    {
        if(_head is null) return;
        var node = _head;
        do
        {
            node.Invalidate();
            node = node.Next!;
        } while (node != _head);
        
        _count = 0;
        ++_version;
    }

    public bool Contains(T value) => Find(value) is not null;

    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        if(arrayIndex < 0 || arrayIndex > array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if(arrayIndex + Count > array.Length) throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
        
        if (_head is null) return;
        var node = _head;
        do
        {
            array[arrayIndex++] = node!.Value; 
            node = node.Next;
        } while (node != _head);
    }

    public bool IsEmpty() => _count == 0;
    
    public CustomLinkedListNode<T>? Find(T value)
    {
        var node = _head;
        EqualityComparer<T> c = EqualityComparer<T>.Default;
        
        if (node is not null)
            do
            {
                if(c.Equals(node!.Value, value)) return node;
                node = node.Next!;
            }while(node != _head);
        
        return null;
    }

    public CustomLinkedListNode<T>? FindLast(T value)
    {
        if (_head is null) return null;
        var last = _head.Prev;
        var node = last;
        EqualityComparer<T> c = EqualityComparer<T>.Default;
        do
        {
            if(c.Equals(node!.Value, value)) return node;
            node = node.Prev;
        } while (node != last);
        
        return null;
    }

    public bool Remove(T value)
    {
        CustomLinkedListNode<T>? node = Find(value);
        if (node is null) return false;
        
        InternalRemoveNode(node);
        return true;
    }

    public void Remove(CustomLinkedListNode<T> node)
    {
        ValidateNode(node);
        InternalRemoveNode(node);
    }

    public void RemoveFirst()
    {
        if(_head is null) throw new InvalidOperationException("The list is empty.");
        InternalRemoveNode(_head);
    }

    public void RemoveLast()
    {
        if(_head is null) throw new InvalidOperationException("The list is empty.");
        InternalRemoveNode(_head.Prev!);
    }
    
    private void InternalRemoveNode(CustomLinkedListNode<T> node)
    {
        Debug.Assert(node.list == this, "Deleting the node from another list!");
        Debug.Assert(_head != null, "This method shouldn't be called on empty list!");

        if (node.Next == node)
        {
            Debug.Assert(_head == node && _count == 1, "This method should be called when the list has only one node, and it is head!");
            _head = null;
        }
        else
        {
            node.Prev!._next = node.Next;
            node.Next!._prev = node.Prev;
            if (node == _head)
                _head = node.Next;
        }
        node.Invalidate();
        --_count;
        ++_version;
    }
    
    public IEnumerator<T> GetEnumerator() => new CustomEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    void ICollection<T>.Add(T item) => AddLast(item);

    bool ICollection<T>.IsReadOnly => false;

    public int Count => _count;
    public CustomLinkedListNode<T>? First => _head;
    public CustomLinkedListNode<T>? Last => _head?.Prev;

    private void InternalInsertNodeBefore(CustomLinkedListNode<T> node, CustomLinkedListNode<T> newNode)
    {
        newNode._next = node;
        newNode._prev = node.Prev;
        node._prev!._next = newNode;
        node._prev = newNode;
        ++_count;
        ++_version;
    }

    private void InternalInsertNodeToEmptyList(CustomLinkedListNode<T> newNode)
    {
        Debug.Assert(_head is null && _count == 0, "The linked list should be empty when this method is called.");
        newNode._next = newNode;
        newNode._prev = newNode;
        _head = newNode;
        ++_count;
        ++_version;
    }
    
    /// <summary>
    /// 节点必须通过new操作符初始化，这意味着节点的_list字段必须为null，否则说明该节点已经属于一个链表了，不能再添加到另一个链表中
    /// </summary>
    /// <param name="node"></param>
    /// <exception cref="InvalidOperationException"></exception>
    private void ValidateNewNode(CustomLinkedListNode<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);
        if (node.list is not null)
            throw new InvalidOperationException("The node already belongs to a list.");
    }
    
    /// <summary>
    /// 节点必须属于当前链表，否则说明该节点不在当前链表中，不能进行添加、删除等操作
    /// </summary>
    /// <param name="node"></param>
    /// <exception cref="InvalidOperationException"></exception>
    private void ValidateNode(CustomLinkedListNode<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);
        if(node.list != this )
            throw new InvalidOperationException("The node is not in the current list.");
    }

    public sealed class CustomLinkedListNode<T>
    {
        public T Value { get; set; }
        internal CustomLinkedListNode<T>? _next;
        internal CustomLinkedListNode<T>? _prev;
        internal CustomLinkedList<T>? list;

        public CustomLinkedListNode<T>? Next => (_next is null || _next.Equals(list!._head)) ? null : _next;
        public CustomLinkedListNode<T>? Prev => (_prev is null || _prev.Equals(list!._head)) ? null : _prev;

    public CustomLinkedListNode(T value)
        {
            Value = value;
        }

        internal CustomLinkedListNode(CustomLinkedList<T> list, T value)
        {
            this.list = list;
            Value = value;
        }

        internal void Invalidate()
        {
            _prev = null;
            _prev = null;
            list = null;
        }
    }
    public struct CustomEnumerator:
        IEnumerator<T>
    {
        private T? _current;
        private CustomLinkedListNode<T>? _node;
        private readonly CustomLinkedList<T> _list;
        private readonly int _version;

        internal CustomEnumerator(CustomLinkedList<T> list)
        {
            _list = list;
            _version = list._version;
            _node = list._head;
            _current = default;
        }
        
        public void Dispose(){}

        public bool MoveNext()
        {
            if(_version != _list._version)
                throw new InvalidOperationException("The collection was modified after the enumerator was created.");
            if (_node is null)
            {
                return false;
            }
            _current = _node.Value;
            _node = _node.Next;

            if (_node == _list._head)
                _node = null;
            return true;
        }

        public void Reset()
        {
            if(_version != _list._version)
                throw new InvalidOperationException("The collection was modified after the enumerator was created.");
            _node = _list._head;
            _current = default;
        }

        T IEnumerator<T>.Current => _current!;

        object? IEnumerator.Current => _current;
    }
}


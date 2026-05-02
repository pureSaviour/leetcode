using System.Diagnostics;

namespace CustomCollection;

public class CustomPriorityQueueView<TElement, TPriority>
{
    private readonly CustomPriorityQueue<TElement, TPriority> _queue;
    private readonly bool _sort;

    public CustomPriorityQueueView(CustomPriorityQueue<TElement, TPriority> queue)
    {
        ArgumentNullException.ThrowIfNull((object)queue, nameof(queue));
        this._queue = queue;
        this._sort = true;
    }

    public CustomPriorityQueueView(
        CustomPriorityQueue<TElement, TPriority>.UnorderedItemsCollection collection)
    {
        _queue = collection?._queue ?? throw new ArgumentNullException(nameof(collection));
    }

    // [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    // public (TElement Element, TPriority Priority)[] Items
    // {
    //     get
    //     {
    //         List<(TElement, TPriority)> valueTupleList = new List<(TElement Element, TPriority Priority)>((IEnumerable<(TElement Element, TPriority Priority)>) this._queue);
    //         if (_sort)
    //             valueTupleList.Sort((Comparison<(TElement, TPriority)>) ((i1, i2) => this._queue.Comparer.Compare(i1.Priority, i2.Priority)));
    //         return valueTupleList.ToArray();
    //     }
    // }
}
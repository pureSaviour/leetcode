using System.Collections.Concurrent;
using System.Diagnostics;

namespace CustomCollection;

public class CustomIProducerConsumerCollectionDebugView<T>
{
    private readonly IProducerConsumerCollection<T> _collection;

    public CustomIProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        
        _collection = collection;
    }
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public T[] Items => _collection.ToArray();
}
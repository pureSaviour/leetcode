using System.Diagnostics;

namespace CustomCollection;

internal sealed class CustomICollectionDebugView<T>
{
    private readonly ICollection<T> _collection;

    public CustomICollectionDebugView(ICollection<T> collection)
    {
        ArgumentNullException.ThrowIfNull((object)collection, nameof(collection));
        this._collection = collection;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public T[] Items
    {
        get
        {
            var array = new T[this._collection.Count];
            this._collection.CopyTo(array, 0);
            return array;
        }
    }
}
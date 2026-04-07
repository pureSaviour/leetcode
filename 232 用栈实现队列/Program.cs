public class MyQueue {
    private readonly Stack<int> _stackIn = new();
    private readonly Stack<int> _stackOut = new();

    public void Push(int x) {
        _stackIn.Push(x);
    }
    
    public int Pop() {
        if (_stackOut.Count != 0) return _stackOut.Pop();
        while (_stackIn.Count > 0)
        {
            _stackOut.Push(_stackIn.Pop());
        }
        return _stackOut.Pop();
    }
    
    public int Peek() {
        if (_stackOut.Count != 0) return _stackOut.Peek();
        while (_stackIn.Count > 0)
        {
            _stackOut.Push(_stackIn.Pop());
        }
        return _stackOut.Peek();
    }
    
    public bool Empty() {
        return _stackIn.Count == 0 && _stackOut.Count == 0;
    }
}
public class Solution {
    public string ProcessStr(string s) {
        LinkedList<char> deque = new LinkedList<char>();
        bool isRev = false;
        foreach (var c in s)
        {
            switch (c)
            {
                case '%':
                    isRev = !isRev;
                    break;
                case '#':
                {
                    char[] charArr = new char[deque.Count];
                    deque.CopyTo(charArr, 0);
                    foreach (var t in charArr)
                        deque.AddLast(t);
                    break;
                }
                case '*':
                {
                    if (deque.Count > 0)
                    {
                        if(isRev)
                            deque.RemoveFirst();
                        else
                            deque.RemoveLast();
                    }
                    break;
                }
                default:
                {
                    if(isRev)
                        deque.AddFirst(c);
                    else
                        deque.AddLast(c);
                    break;
                }
            }
        }

        char[] res = deque.ToArray();
        if(isRev)
            Array.Reverse(res);
        return new string(res);
    }
}
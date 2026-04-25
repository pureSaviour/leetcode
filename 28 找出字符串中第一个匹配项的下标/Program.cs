public class Solution
{
    private ulong[] _pow;
    private ulong[] _hash;
    const int Base = 331;
    public int StrStr(string haystack, string needle) {
        var hn = haystack.Length;
        _pow = new ulong[hn];
        _hash = new ulong[hn];
        _pow[0] = 1;
        _hash[0] = haystack[0];
        for (int i = 1; i < hn; ++i)
        {
            _pow[i] = _pow[i - 1] * Base;
            _hash[i] = _hash[i - 1] * Base + haystack[i];
        }
        
        var nHashCode = GetStrHashCode(needle);
        for (int i = 0; i <= hn - needle.Length; ++i)
        {
            var hashCode = GetSubStrHashCode(i, i + needle.Length - 1);
            if (hashCode == nHashCode)
                return i;
        }
        return -1;
    }

    private ulong GetSubStrHashCode(int l, int r)
    {
        if (l == 0)
            return _hash[r];
        return _hash[r] - _hash[l - 1] * _pow[r - l + 1];
    }
    
    private static ulong GetStrHashCode(string s)
    {
        ulong hashCode = 0;
        foreach (var c in s)
            hashCode = hashCode * Base + c;
        return hashCode;
    }
}
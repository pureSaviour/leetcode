using System.Text;

public class Solution
{
    private ulong[] _hash;
    private ulong[] _pow;
    private const int Base = 331;

    public int RepeatedStringMatch(string a, string b)
    {
        var an = a.Length;
        var bn = b.Length;
        var bHashCode = GetStringHashCode(b);
        var minRepeat = (an + bn - 1) / an + 1;
        StringBuilder sb = new StringBuilder(minRepeat);
        for (int i = 0; i < minRepeat; ++i)
            sb.Append(a);
        var s = sb.ToString();
        var sn = s.Length;
        _hash = new ulong[sn];
        _pow = new ulong[sn];
        _pow[0] = 1;
        _hash[0] = s[0];
        for (int i = 1; i < sn; ++i)
        {
            _pow[i] = _pow[i - 1] * Base;
            _hash[i] = _hash[i - 1] * Base + s[i];
        }

        for (int i = 0; i <= sn - bn; ++i)
        {
            var hashCode = GetSubStrHashCode(i, i + bn - 1);
            if (hashCode == bHashCode)
                return i + bn <= an * (minRepeat - 1) ? minRepeat - 1 : minRepeat;
        }

        return -1;
    }
    private ulong GetSubStrHashCode(int l, int r)
    {
        if (l == 0) return _hash[r];
        return _hash[r] - _hash[l - 1] * _pow[r - l + 1];
    }
    private ulong GetStringHashCode(string s)
    {
        ulong hashCode = 0;
        foreach (var c in s)
            hashCode = hashCode * Base + c;
        return hashCode;
    }
}
Solution sol = new();
string s = "defdefdefabcabc", goal = "defdefabcabcdef";
Console.WriteLine(sol.RotateString(s, goal).ToString());

public class Solution
{
    private const int Base = 331;
    public bool RotateStringWithHash(string s, string goal)
    {
        int sn = s.Length;
        int gn = goal.Length;
        if (sn != gn) return false;
        
        ulong[] pow = new ulong[sn];
        ulong[] sHash = new ulong[sn];
        pow[0] = 1;
        sHash[0] = s[0];
        
        ulong gHash = goal[0];
        
        for (int i = 1; i < sn; ++i)
        {
            pow[i] = pow[i - 1] * Base;
            sHash[i] = sHash[i - 1] * Base + s[i];
            gHash = gHash * Base + goal[i];
        }

        for (int i = 0; i < sn; ++i)
        {
            var curSHash = GetHash(i, sn - 1, sHash, pow) * pow[i] + GetHash(0, i - 1, sHash, pow);
            if(curSHash == gHash) return true;
        }
        return false;
    }
    
    private static ulong GetHash(int l, int r, ulong[] sHash, ulong[] pow)
    {
        if (r < l) return 0;
        if (l == 0) return sHash[r];
        return sHash[r] - sHash[l - 1] * pow[r - l + 1];
    }

    public bool RotateString(string s, string goal)
    {
        return s.Length == goal.Length && (s + s).Contains(goal);
    }
}
Solution sol = new();
var start = 0;
var n = 5;
var res = sol.XorOperation(n, start);
Console.WriteLine(res);

public class Solution {
    public int XorOperation(int n, int start) {
        var res = start;
        var num = start;
        for(int i = 1; i < n; ++i){
            num += 2;
            res ^= num;
        }
        return res;
    }
}
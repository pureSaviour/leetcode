Solution sol = new();
string[] queries = ["word","note","ants","wood"];
string[] dictionary = ["wood","joke","moat"];
var res = sol.TwoEditWords(queries, dictionary);
foreach (var word in res)    Console.WriteLine(word);

public class Solution {
    public IList<string> TwoEditWords(string[] queries, string[] dictionary) {
        List<string> res = new(queries.Length);
        foreach (var query in queries)
        foreach (var word in dictionary)
            if (IsTwoEditDistance(query, word))
            {
                res.Add(query);
                break;
            }
        return res;
    }
    
    private static bool IsTwoEditDistance(string s1, string s2)
    {
        for (int i = 0, count = 0; i < s1.Length; i++)
            if (s1[i] != s2[i] && ++count > 2) return false;
        return true;
    }
}
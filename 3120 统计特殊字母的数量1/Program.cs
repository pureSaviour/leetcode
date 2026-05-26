Solution sol = new();
string word = "aaAbcBC";
Console.WriteLine(sol.NumberOfSpecialChars(word));


public class Solution {
    public int NumberOfSpecialChars(string word)
    {
        bool[] visited = new bool[52];
        int count = 0;
        foreach (var c in word)
        {
            var index = GetIndex(c);
            if (index < 26 && visited[index + 26] && !visited[index] || index >= 26 && visited[index - 26] && !visited[index])
                ++count;
            visited[index] = true;
        }

        return count;
    }

    private static int GetIndex(char c) => char.IsLower(c) ? c - 'a' : c - 'A' + 26;
}
public class Solution {
    public string MapWordWeights(string[] words, int[] weights) => new string(words.Select(w => (char)('z' - w.Sum(c => weights[c - 'a']) % 26)).ToArray());
}
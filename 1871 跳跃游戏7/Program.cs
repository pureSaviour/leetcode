public class Solution {
    public bool CanReach(string s, int minJump, int maxJump) {
        int n = s.Length;
        bool[] visited = new bool[n];
        visited[0] = true;
        for(int i = 0, j = 1; i < n && j < n; ++i){
            if(visited[i] && s[i] == '0'){
                for(j = Math.Max(j, i + minJump); j <= Math.Min(i + maxJump, n - 1);++j){
                    visited[j] = true;            
                }
            }
        }
        return visited[n - 1] && s[n -  1] == '0';
    }
}
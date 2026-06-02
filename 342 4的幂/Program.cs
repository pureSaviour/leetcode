public class Solution {
    public bool IsPowerOfFour(int n) => n > 0 && (n & -n) == n && (0xAAAAAAAA & n) == 0;
}
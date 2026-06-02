public class Solution {
    public int RangeBitwiseAnd(int left, int right) {
        while(right > left)
            right -= (right & -right);    
        return right;
    }
}
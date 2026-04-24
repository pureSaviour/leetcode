public class Solution {
    public int FurthestDistanceFromOrigin(string moves) {
        int l = 0, r = 0, _ = 0;
        foreach (var c in moves)
            switch (c)
            {
                case 'L':
                    ++l; break;
                case 'R':
                    ++r; break;
                case '_':
                    ++_; break;
                default:
                    throw new NotImplementedException();
            }
        return Math.Abs(l - r) + _;
    }
}
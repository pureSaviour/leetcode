public class Solution {
    public double AngleClock(int hour, int minutes) {
        double hDegree = (hour % 12 + (minutes % 60) / 60d) * 30;
        double mDegree = (minutes % 60) * 6d;
        return Math.Min(Math.Abs(hDegree - mDegree), 360 - Math.Abs(hDegree - mDegree));
    }
}
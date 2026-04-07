Solution sol = new();
var students = new int[] {1,1,0,0}; 
var sandwiches = new int[] {0,1,0,1};
var isValid = sol.CountStudents(students, sandwiches);
Console.WriteLine(isValid);

public class Solution {
    public int CountStudents(int[] students, int[] sandwiches)
    {
        int[] counts = [0, 0];
        foreach (var student in students)
        {
            counts[student]++;
        }
        for (int i = 0; i < sandwiches.Length; i++) {
            if (--counts[sandwiches[i]] == -1) return sandwiches.Length - i;
        }
        return 0;
    }
}
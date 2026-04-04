Solution sol = new Solution();
var str = "()[]{}";
var isValid = sol.IsValid(str);
Console.WriteLine(isValid);

public class Solution {
    public bool IsValid(string s)
    {
        Stack<char> stack = new Stack<char>();
        foreach (var c in s)
        {
            if(c is '(' or '[' or '{')
            {
                stack.Push(c);
            }
            else
            {
                if (c is ')' or ']' or '}')
                {
                    if (stack.Count == 0) return false;
                    char top = stack.Pop();
                    if ((c == ')' && top != '(') || (c == ']' && top != '[') || (c == '}' && top != '{'))
                    {
                        return false;
                    }
                }
            }
        }
        
        return stack.Count == 0;
    }
}
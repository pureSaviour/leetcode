public class Solution {
    public string ToLowerCase(string s)
    {
        // 大写变小写、小写变大写 : 字符 ^= 32;
        // 大写变小写、小写变小写 : 字符 |= 32;
        // 小写变大写、大写变大写 : 字符 &= -33;
        
        var charArray = s.ToCharArray();
        for (int i = 0; i < charArray.Length; ++i)
            if(char.IsLetter(charArray[i]))
                charArray[i] |= (char)32;
        
        return new string(charArray);
    }
}
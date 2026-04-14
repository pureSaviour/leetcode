Solution sol = new();
string word = "USA";
var res = sol.DetectCapitalUse(word);
Console.WriteLine(res.ToString());

public class Solution {
    public bool DetectCapitalUse(string word) {
        if (word.Length < 2) return true;
        bool isFirstCapital = char.IsUpper(word[0]);
        bool isSecondCapital = char.IsUpper(word[1]);
        if (!isFirstCapital && isSecondCapital) return false;     // 首字母小写，第二个字母大写
        for (int i = 2; i < word.Length; i++)
        {
            var isCapital = char.IsUpper(word[i]);
            if(isCapital && isFirstCapital && isSecondCapital) continue;        // 全大写
            if(!isCapital && isFirstCapital && !isSecondCapital) continue;      // 首字母大写，其他小写
            if(!isCapital && !isFirstCapital && !isSecondCapital) continue;     // 全小写
            return false;
        }
        return true;
    }
}
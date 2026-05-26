public class Solution {
    public string CapitalizeTitle(string title)
    {
        bool newWord = true;
        string[] words = title.Split(' ');
        for (int i = 0; i < words.Length; ++i)
        {
            words[i] = words[i].ToLower();
            if (words[i].Length > 2)
            {
                var charArray = words[i].ToCharArray();
                charArray[0] = char.ToUpper(charArray[0]);
                words[i] = new string(charArray);
            }
        }
        return string.Join(' ', words);
    }
}
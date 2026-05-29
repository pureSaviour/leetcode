public class Solution {
    public int[] StringIndices(string[] wordsContainer, string[] wordsQuery)
    {
        int cn = wordsContainer.Length;
        int qn = wordsQuery.Length;
        TreeNode root = new TreeNode();
        int[] res = new int[qn];
        int minLen = int.MaxValue;
        int minIdx = 0;
        for (int i = 0; i < cn; i++)
        {
            int len = wordsContainer[i].Length;
            if (len < minLen)
            {
                minLen = len;
                minIdx = i;
            }
        }
        root.Value = new TreeNode.Tuple { Index = minIdx, Length = minLen };
         
        // 建字典树
        for (int i = 0; i < cn; ++i)
        {
            var str = wordsContainer[i];
            TreeNode node = root;
            for (int j = str.Length - 1; j >= 0; --j)
            {
                TreeNode? child = null;
                var index = str[j] - 'a';
                child = node.Children[index];

                if (child is null)
                {
                    child = new TreeNode()
                        { Value = new TreeNode.Tuple() { Index = i, Length = str.Length }, Key = str[j] };
                    node.Children[index] = child;
                }
                else if (child.Value.Length > str.Length || (child.Value.Length == str.Length && i < child.Value.Index))
                    child.Value = new TreeNode.Tuple() { Index = i, Length = str.Length };
                

                node = child;
            }
        }

        for (int i = 0; i < qn; ++i)
        {
            var query = wordsQuery[i];
            TreeNode node = root;

            for (int j = query.Length - 1; j >= 0; --j)
            {
                int index = query[j] - 'a';
                TreeNode? child = node.Children[index];
                
                if (child is null) break;
                
                node = child;
            }
            
            res[i] = node.Value.Index;
        }

        return res;
    }
    
    public class TreeNode
    {
        public readonly TreeNode?[] Children = new TreeNode?[26];
        public Tuple Value { get; set; }
        public char Key { get; set; }

        public struct Tuple
        {
            public int Index { get; set; }
            public int Length { get; set; }
        }
    }
}
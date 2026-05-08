Solution sol = new();
int[] nums =
[
    57194, 8004, 14132, 30003, 47500, 5064, 59483, 22960, 49346, 43671, 43081, 29406, 9744, 5406, 11407, 9861, 33697,
    19764, 57674, 16580, 18030, 39463, 59688, 30761, 2358, 7726, 31818, 30575, 2643, 13593, 36523, 54211, 41058, 54220,
    37757, 22908, 55452, 18525, 51624, 15709, 54132, 5444, 426, 49248, 53420, 23035, 6169, 2415, 50206, 33514, 52364,
    28175, 26674, 26573, 21019, 58649, 6639, 55799, 1908, 16578, 50190, 48038, 49830, 30916, 7139, 28792, 32264, 34388,
    56377, 20421, 20896, 4060, 16404, 37769, 25426, 15913, 29497, 8314, 41851, 4052, 31661, 23215, 8582, 18793, 33771,
    57549, 2909, 3482, 36175, 59980, 32985, 10465, 19625, 50168, 42809, 57302, 30217, 58625, 46283, 2887, 2564, 17104,
    33872, 41508, 22935, 6548, 38600, 37998, 7928, 10583, 20441, 27863, 29800, 33139, 29661, 57246, 15108, 18825, 43141,
    13838, 33553, 18792, 14430, 36726, 51623, 32084, 25707, 48985, 41805, 26556, 4599, 36867, 15803, 20208, 28946,
    59237, 9182, 51699, 56551, 22960, 49988, 19050, 40550, 42309, 30565, 1529, 42530, 9345, 20583, 37951, 14291, 8792,
    16583, 13454, 14504, 22392, 2945, 24074, 43738, 47168, 1738, 25186, 17965, 46501, 55953, 11048, 55812, 16411, 4286,
    56138, 13194, 3322, 40165, 22551, 22181, 35157, 42702, 18707, 49930, 654, 43881, 16958, 52566, 8116, 27702, 51784,
    22750, 9120, 41640, 47252, 54469, 35251, 40002, 36668, 46829, 38022, 2459, 22799, 55308, 37639, 19670, 21828, 9724,
    14143, 39466, 26340, 16692, 37080, 3124, 46038, 14807, 59685, 10672, 646, 42266, 25224, 34405, 13671, 38499, 9855,
    48732, 26748, 38715, 7080, 36194, 16234, 3287, 50620, 46239, 42819, 48429, 41890, 34194, 45824, 32993, 16824, 51268,
    44660, 2261, 52426, 38124, 37197, 51385, 25359, 7197
];
Console.WriteLine(sol.MinJumps(nums));

public class Solution {
    public int MinJumps(int[] nums)
    {
        int n = nums.Length;
        int max = int.MinValue;
        for(int i = 0; i < n; ++i)
            max = Math.Max(nums[i], max);
        
        // 获取每个数的最小质因数，初始化默认每个数的最小质因数为它自己
        int[] primes = new int[max + 1];
        for(int i =0; i <= max; ++i)
            primes[i] = i;          
        // 这里要埃氏筛要注意边界条件，首先求n以内的质数，只需要看i <= sqrt(n)的部分，所以这里外层循环的条件是i * i <= max
        // 内部循环的起始位置是i * i，因为(i - n) * i的部分在判断i - n就已经筛选过了
        for(int i = 2; i * i <= max ; ++i)
            if(IsPrime(i))
                for (int j = i * i; j <= max; j += i)
                    if(primes[j] == j)
                        primes[j] = i;

        List<int>?[] primeIndicesMap = new List<int>[max + 1];
        for (int i = 0; i < n; ++i)
        {
            var num = nums[i];
            while (num > 1)
            {
                int minPrime = primes[num];
                primeIndicesMap[minPrime] ??= [];
                primeIndicesMap[minPrime]!.Add(i);
                while (num > 0 && num % minPrime == 0)
                    num /= minPrime;
            }
        }
        
        Queue<int> queue = new Queue<int>();
        bool[] visited = new bool[n];
        bool[] visitedPrimes = new bool[max + 1];
        queue.Enqueue(0);
        int step = 0;
        while (queue.Count > 0)
        {
            int count = queue.Count;
            for (int i = 0; i < count; ++i)
            {
                int index = queue.Dequeue();
                if (index == n - 1)
                    return step;
                int[] neighbors = [index - 1, index + 1];
                foreach (var neighbor in neighbors)
                    if (neighbor < n && neighbor >= 0 && !visited[neighbor])
                    {
                        queue.Enqueue(neighbor);
                        visited[neighbor] = true;
                    }

                int num = nums[index];
                if (num > 1 && primes[num] == num && !visitedPrimes[num])
                {
                    visitedPrimes[num] = true;
                    var primeIndices = primeIndicesMap[num];
                    if (primeIndices is null) 
                        continue;
                    foreach (var primeIndex in primeIndices.Where(primeIndex => !visited[primeIndex]))
                    {
                        queue.Enqueue(primeIndex);
                        visited[primeIndex] = true;
                    }
                }
            }

            ++step;
        }

        return -1;
    }

    private static bool IsPrime(int n)
    {
        var l = (int)Math.Sqrt(n);
        for (int i = 2; i < l; ++i)
            if(n % i == 0)
                return false;
        return true;
    }
}
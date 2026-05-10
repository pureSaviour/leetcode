public class Solution {
    public int[] ScoreValidator(string[] events)
    {
        int count = 0;
        int score = 0;

        foreach (var @event in events)
        {
            if (@event == "W")
            {
                if(++count == 10)
                    break;
            }
            else if (@event is "WD" or "NB")
                ++score;
            else score += int.Parse(@event);
        }
        
        return [score, count];
    }
}
using System.Collections.Generic;

public class MatchDicComparer : IComparer<KeyValuePair<int, Match>>
{
    public int Compare(KeyValuePair<int, Match> x, KeyValuePair<int, Match> y)
    {
        return x.Value.DDay.GetHashCode().CompareTo(y.Value.DDay.GetHashCode());
    }
}

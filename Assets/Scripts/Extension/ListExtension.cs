using System;
using System.Collections;
using System.Collections.Generic;

public static class ListExtension
{
    public static void Shuffle(this List<int> list)
    {
        Random r = new(DateTime.Now.Millisecond);
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = r.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

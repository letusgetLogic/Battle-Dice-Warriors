using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    private static readonly Random _random = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = _random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
    // Usage:
    //var rangeList = Enumerable.Range(1, 100).ToList(); // List of 1 to 100
    //rangeList.Shuffle(); // Shuffles in place
}

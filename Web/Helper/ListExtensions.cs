using System;
using System.Collections.Generic;

namespace Web.Helper
{
    public static class ListExtensions
    {
        private static readonly Random _rng = new();

        // Fisher–Yates shuffle (in-place)
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}

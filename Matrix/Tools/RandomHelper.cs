using System;
using System.Collections.Generic;

namespace Matrix.Tools
{
    public static class RandomHelper
    {
        public static bool Chance(this Random random, double chance)
        {
            // lock (random)
            // {
                return random.NextDouble() < chance;
            // }
        }

        public static T Choice<T>(this Random random, IList<T> array)
        {
            return array[random.Next(array.Count)];
        }

        public static double NextDouble(this Random random, double a, double b) 
            => random.NextDouble() * (b - a) + a;
    }
}
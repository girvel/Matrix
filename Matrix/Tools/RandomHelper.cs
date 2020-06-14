using System;

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

        public static double NextDouble(this Random random, double a, double b) 
            => random.NextDouble() * (b - a) + a;
    }
}
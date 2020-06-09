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
    }
}
using System;
using Matrix.Tools;
using Precalc;

namespace Matrix.Core.Systems
{
    public class Evaporation : System
    {
        [Constant] public double BasicChance;

        public override void Update()
        {
            foreach (var (v, region) in Session.Field)
            {
                if (region.Terrain.Water <= 0 || !(Session.Random.NextDouble() < BasicChance)) continue;
                region.Terrain.Water--;
                region.Terrain.Clouds++;
            }
        }
    }
}
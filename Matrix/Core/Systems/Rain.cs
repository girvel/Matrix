using System;
using Matrix.Tools;

namespace Matrix.Core.Systems
{
    public class Rain : System
    {
        [Constant] public double BasicChance;
        
        public override void Update()
        {
            foreach (var (v, region) in Session.Field)
            {
                region.IsRaining 
                    = Session.Random.NextDouble() < BasicChance * (1 - Math.Pow(1.054, -region.Terrain.Clouds)); // TODO: precalc
                if (!region.IsRaining) continue;
                region.Terrain.Clouds--;
                region.Terrain.Water++;
            }
        }
    }
}
using System;
using Matrix.Tools;

namespace Matrix.Core.Systems
{
    public class Volcanoes : System
    {
        [Constant] public double BasicChance;

        public override void Update()
        {
            foreach (var (v, region) in Session.Field)
            {
                if (Session.Random.NextDouble() < BasicChance)
                {
                    region.Terrain.Lava += region.LavaPotential;
                }
            }
        }
    }
}
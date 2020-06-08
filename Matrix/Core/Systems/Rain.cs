using System;
using Matrix.Tools;
using Precalc;

namespace Matrix.Core.Systems
{
    public class Rain : System
    {
        [Constant] public double BasicChance;

        public readonly NaturalFunction<double> ChanceFunction;

        public Rain()
        {
            ChanceFunction = new NaturalFunction<double>(
                clouds => BasicChance * (1 - Math.Pow(1.054, -clouds)),
                10);
        }

        public override void Update()
        {
            foreach (var (v, region) in Session.Field)
            {
                region.IsRaining = Session.Random.Chance(ChanceFunction.Calculate(region.Terrain.Clouds)); // TODO: precalc
                if (!region.IsRaining) continue;
                region.Terrain.Clouds--;
                region.Terrain.Water++;
            }
        }
    }
}
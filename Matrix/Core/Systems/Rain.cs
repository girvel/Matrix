using System;
using Angem;
using Matrix.Tools;
using Precalc;

namespace Matrix.Core.Systems
{
    public class Rain : RegionSystem
    {
        [Constant] public double BasicChance;

        public readonly NaturalFunction<double> ChanceFunction;

        public Rain()
        {
            ChanceFunction = new NaturalFunction<double>(
                clouds => BasicChance * (1 - Math.Pow(1.054, -clouds)),
                10);
        }

        protected override void UpdateEntity(int2 position, Region region)
        {
            region.IsRaining = Session.Random.Chance(ChanceFunction.Calculate(region.Terrain.Clouds));
            if (!region.IsRaining) return;
            region.Terrain.Clouds--;
            region.Terrain.Water++;
        }
    }
}
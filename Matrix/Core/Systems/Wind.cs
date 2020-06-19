using System;
using Angem;
using Matrix.Tools;
using Precalc;

namespace Matrix.Core.Systems
{
    public class Wind : RegionSystem
    {
        public readonly int2[] Directions =
            {
                int2.Right,
            },
            AdditionalDirections =
            {
                int2.Forward,
                int2.Back,
            };

        public readonly NaturalFunction<double>[] ChanceFunctions;

        public Wind()
        {
            ChanceFunctions = new NaturalFunction<double>[Terrain.Size];

            for (var i = 0; i < ChanceFunctions.Length; i++)
            {
                var chance = BasicChances[i];
                ChanceFunctions[i] = new NaturalFunction<double>(
                    delta => chance * Math.Pow(1.41, delta),
                    20,
                    -10);
            }
        }

        [Constant] public double[] BasicChances;
        
        public void MoveGas(int2 v, Region region, byte gas)
        {
            foreach (var d in State.Random.Choice(State.RandomizedDirections))
            {
                if (!(v + d).Inside(State.Field.Size)) continue;
                var other = State.Field[v + d];

                if (region.Pressure <= other.Pressure
                    || !State.Random.Chance(BasicChances[gas])) continue;

                other.Terrain.Clouds += region.Terrain.Clouds;
                region.Terrain.Clouds = 0;
            }
        }

        protected override void UpdateEntity(int2 position, Region region)
        {
            for (byte i = 0; i < Terrain.Size; i++)
            {
                if (BasicChances[i] > 0)
                    MoveGas(position, region, i);
            }
        }
    }
}
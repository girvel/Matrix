using System;
using Matrix.Tools;
using Precalc;

namespace Matrix.Core.Systems
{
    public class Wind : System
    {
        public Vector2[] Directions =
            {
                Vector2.Right,
            },
            AdditionalDirections =
            {
                Vector2.Up,
                Vector2.Down,
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
        
        public void MoveGas(byte gas)
        {
            foreach (var (v, region) in Session.Field)
            {
                foreach (var dir in Directions)
                {
                    if (!(v + dir).Inside(Session.Field.Size)) continue;

                    var other = Session.Field[v + dir];
                    
                    if (!Session.Random.Chance(
                        ChanceFunctions[gas].Calculate(
                            region.Terrain.SliceFrom(Terrain.CLOUDS) 
                            - other.Terrain.SliceFrom(Terrain.CLOUDS) - 2)))
                        continue;

                    other.Terrain.Clouds += region.Terrain.Clouds;
                    region.Terrain.Clouds = 0;
                    break;
                }

                foreach (var dir in AdditionalDirections)
                {
                    if (!(v + dir).Inside(Session.Field.Size)) continue;

                    var other = Session.Field[v + dir];
                    if (!Session.Random.Chance(
                        ChanceFunctions[gas].Calculate(
                            region.Terrain.SliceFrom(Terrain.CLOUDS) 
                            - other.Terrain.SliceFrom(Terrain.CLOUDS) - 2) / 2)) continue;

                    other.Terrain.Clouds += region.Terrain.Clouds;
                    region.Terrain.Clouds = 0;
                    break;
                }
            }
        }

        [Constant] public double[] BasicChances;
        
        public override void Update()
        {
            for (byte i = 0; i < Terrain.Size; i++)
            {
                if (BasicChances[i] > 0)
                    MoveGas(i);
            }
        }
    }
}
using System;
using Matrix.Tools;

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
        
        public void MoveGas(byte gas, double maximalChance)
        {
            foreach (var (v, region) in Session.Field)
            {
                foreach (var dir in Directions)
                {
                    if (!(v + dir).Inside(Session.Field.Size)) continue;

                    var other = Session.Field[v + dir];
                    if (Session.Random.NextDouble() >=
                        maximalChance * Math.Pow(
                            1.41, 
                            region.Terrain.SliceFrom(Terrain.CLOUDS)
                            - other.Terrain.SliceFrom(Terrain.CLOUDS) - 2))  // TODO: precalc
                        continue;

                    other.Terrain.Clouds += region.Terrain.Clouds;
                    region.Terrain.Clouds = 0;
                    break;
                }

                foreach (var dir in AdditionalDirections)
                {
                    if (!(v + dir).Inside(Session.Field.Size)) continue;

                    var other = Session.Field[v + dir];
                    if (Session.Random.NextDouble() >=
                        maximalChance / 2 * Math.Pow(1.41, region.Terrain.Clouds - other.Terrain.SliceFrom())) continue; // TODO: precalc

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
                    MoveGas(i, BasicChances[i]);
            }
        }
    }
}
using System;
using Matrix.Tools;

namespace Matrix.Core.Systems
{
    public class Flow : System
    {
        public Vector2[] Directions =
        {
            Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left,
        };
        
        public void MoveFluid(byte fluid, double chance)
        {
            foreach (var (v, region) in Session.Field)
            {
                region.FlowDirection[fluid] = Vector2.Zero;
                foreach (var dir in Directions)
                {
                    if (region.Terrain[fluid] == 0) break;
                    if (!(v + dir).Inside(Session.Field.Size)) continue;

                    var other = Session.Field[v + dir];
                    
                    var regionSum = region.Terrain.SliceFrom(fluid);
                    var otherSum = other.Terrain.SliceFrom(fluid);
                    
                    if (regionSum - otherSum > 1 && Session.Random.NextDouble() < chance
                        || regionSum - otherSum == 1 && Session.Random.NextDouble() < chance / 2)
                    {
                        other.Terrain[fluid]++;
                        region.Terrain[fluid]--;
                        region.FlowDirection[fluid] = dir;
                    }
                }
            }
        }

        [Constant] public double[] BasicChances;
        
        public override void Update()
        {
            for (byte i = 0; i < Terrain.Size; i++)
            {
                if (BasicChances[i] > 0)
                    MoveFluid(i, BasicChances[i]);
            }
        }
    }
}
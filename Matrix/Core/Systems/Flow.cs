using Matrix.Tools;
using NotImplementedException = System.NotImplementedException;

namespace Matrix.Core.Systems
{
    public class Flow : RegionSystem
    {
        public int2[] Directions =
        {
            int2.Up, int2.Right, int2.Down, int2.Left,
        };
        
        public void MoveFluid(int2 v, Region region, byte fluid, double chance)
        {
            region.FlowDirection[fluid] = int2.Zero;
            foreach (var dir in Directions)
            {
                if (region.Terrain[fluid] == 0) break;
                if (!(v + dir).Inside(Session.Field.Size)) continue;

                var other = Session.Field[v + dir];
                
                var regionSum = region.Terrain.SliceFrom(fluid);
                var otherSum = other.Terrain.SliceFrom(fluid);
                
                if (regionSum - otherSum > 1 && Session.Random.Chance(chance)
                    || regionSum - otherSum == 1 && Session.Random.Chance(chance / 2))
                {
                    other.Terrain[fluid]++;
                    region.Terrain[fluid]--;
                    region.FlowDirection[fluid] = dir;
                }
            }
        }

        [Constant] public double[] BasicChances;

        protected override void UpdateEntity(int2 position, Region region)
        {
            for (byte i = 0; i < Terrain.Size; i++)
            {
                if (BasicChances[i] > 0)
                    MoveFluid(position, region, i, BasicChances[i]);
            }
        }
    }
}
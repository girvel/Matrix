using System.Linq;
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

        public int2[][] RandomizedDirections;

        public Flow()
        {
            RandomizedDirections = Enumerable
                .Range(0, Directions.Length)
                .Select(i => Enumerable
                    .Range(i, Directions.Length)
                    .Select(j => Directions[j % Directions.Length])
                    .ToArray())
                .ToArray();
        }
        
        public void MoveFluid(int2 v, Region region, byte fluid, double chance)
        {
            region.FlowDirection[fluid] = int2.Zero;
            foreach (var dir in RandomizedDirections[Session.Random.Next(Directions.Length)])
            {
                if (region.Terrain[fluid] == 0) break;
                if (!(v + dir).Inside(Session.Field.Size)) continue;

                var other = Session.Field[v + dir];
                
                var regionSum = region.Terrain.SliceFrom(fluid);
                var otherSum = other.Terrain.SliceFrom(fluid);

                if (regionSum - otherSum <= 0 || !Session.Random.Chance(chance)) continue;
                
                other.Terrain[fluid]++;
                region.Terrain[fluid]--;
                region.FlowDirection[fluid] = dir;
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
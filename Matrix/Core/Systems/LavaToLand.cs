using Matrix.Tools;
using NotImplementedException = System.NotImplementedException;

namespace Matrix.Core.Systems
{
    public class LavaToLand : RegionSystem
    {
        public readonly int2[] Directions =
        {
            int2.Zero, int2.Up, int2.Right, int2.Down, int2.Left,
        };

        protected override void UpdateEntity(int2 v, Region region)
        {
            if (Session.Random.NextDouble() >= 0.95) return;

            foreach (var dir in Directions)
            {
                if (region.Terrain[Terrain.LAVA] <= 0) break;
                if (!(v + dir).Inside(Session.Field.Size)) continue;
                    
                var other = Session.Field[v + dir];
                    
                if (other.Terrain[Terrain.WATER] <= 0) continue;
                    
                region.Terrain[Terrain.LAND]++;
                region.Terrain[Terrain.LAVA]--;
                other.Terrain[Terrain.WATER]--;
                other.Terrain[Terrain.CLOUDS]++;
                break;
            }

            if (region.Terrain[Terrain.LAVA] > 0 && Session.Random.NextDouble() < 0.1)
            {
                region.Terrain[Terrain.LAND]++;
                region.Terrain[Terrain.LAVA]--;
            }
        }
    }
}
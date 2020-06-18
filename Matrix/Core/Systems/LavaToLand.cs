using Angem;
using Matrix.Tools;

namespace Matrix.Core.Systems
{
    public class LavaToLand : RegionSystem
    {
        public readonly int2[] Directions =
        {
            int2.Zero, int2.Forward, int2.Right, int2.Back, int2.Left,
        };

        [Constant] public double BasicChance, AdditionalChance;

        protected override void UpdateEntity(int2 v, Region region)
        {
            if (!State.Random.Chance(BasicChance)) return;

            foreach (var dir in Directions)
            {
                if (region.Terrain[Terrain.LAVA] <= 0) break;
                if (!(v + dir).Inside(State.Field.Size)) continue;
                    
                var other = State.Field[v + dir];
                    
                if (other.Terrain[Terrain.WATER] <= 0) continue;
                    
                region.Terrain[Terrain.LAND]++;
                region.Terrain[Terrain.LAVA]--;
                other.Terrain[Terrain.WATER]--;
                other.Terrain[Terrain.CLOUDS]++;
                break;
            }

            if (region.Terrain[Terrain.LAVA] <= 0 || !State.Random.Chance(AdditionalChance)) return;
            
            region.Terrain[Terrain.LAND]++;
            region.Terrain[Terrain.LAVA]--;
        }
    }
}
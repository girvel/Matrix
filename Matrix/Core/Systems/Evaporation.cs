using Angem;
using Matrix.Tools;

namespace Matrix.Core.Systems
{
    public class Evaporation : RegionSystem
    {
        [Constant] public double BasicChance;

        protected override void UpdateEntity(int2 position, Region region)
        {
            if (region.Terrain.Water <= 0 || !(Session.Random.Chance(BasicChance))) return;
            region.Terrain.Water--;
            region.Terrain.Clouds++;
        }
    }
}
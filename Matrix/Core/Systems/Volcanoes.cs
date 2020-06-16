using Angem;
using Matrix.Tools;
using NotImplementedException = System.NotImplementedException;

namespace Matrix.Core.Systems
{
    public class Volcanoes : RegionSystem
    {
        [Constant] public double BasicChance;

        protected override void UpdateEntity(int2 position, Region region)
        {
            if (Session.Random.Chance(BasicChance))
            {
                region.Terrain.Lava += region.LavaPotential;
            }
        }
    }
}
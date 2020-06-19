using System;
using System.Linq;
using Angem;
using Matrix.Core;
using Matrix.Tools;

namespace Matrix.Core.Systems
{
    public class Pressure : RegionSystem
    {
        [Constant] public double CloudsK, NearCloudsK;
        
        protected override void UpdateEntity(int2 position, Region region)
        {
            region.Pressure = CloudsK * region.Terrain.Clouds
                              + 1.0 / region.Terrain.SliceFrom(Terrain.WATER)
                              + NearCloudsK / State.Directions
                                  .Where(d => (position + d).Inside(State.Field.Size))
                                  .Sum(d => State.Field[position + d].Terrain.Clouds);
        }
    }
}
using Angem;
using Matrix.Tools;

namespace Matrix.Core
{
    public class Region
    {
        public readonly Terrain Terrain = new Terrain(0, 4, 0, 0);

        public byte LavaPotential = 1; // TODO to LavaChance

        public bool 
            IsRaining = false;

        public readonly int2[] FlowDirection = new int2[Terrain.Size];
    }
}
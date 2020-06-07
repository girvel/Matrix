using Matrix.Tools;

namespace Matrix.Core
{
    public class Region
    {
        public Terrain Terrain = new Terrain(0, 4, 0, 0);

        public byte LavaPotential = 100;

        public bool 
            IsRaining = false;

        public readonly Vector2[] FlowDirection = new Vector2[Terrain.Size];
    }
}
using System.Net.NetworkInformation;
using Matrix.Tools;

namespace Matrix.Core
{
    public class Region
    {
        public Terrain Terrain = new Terrain(2, 0, 0);

        public byte LavaPotential = 25;
    }
}
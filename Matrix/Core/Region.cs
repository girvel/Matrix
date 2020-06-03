using System.Net.NetworkInformation;
using Matrix.Tools;

namespace Matrix.Core
{
    public class Region
    {
        public Pile<string, int> Terrain = new Pile<string, int>
        {
            ["water"] = 2,
            ["lava"] = 0,
            ["land"] = 0,
        };

        public int LavaPotential = 25;
    }
}
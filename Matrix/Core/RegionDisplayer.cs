using System;
using System.Linq;
using Matrix.Tools;

namespace Matrix.Core
{
    public class RegionDisplayer
    {
        public (char character, ConsoleColor foreground, ConsoleColor background) RegionToConsoleAtom(Region region)
        {
            if (region.Terrain[Terrain.Water] > 0)
            {
                if (region.Terrain[Terrain.Lava] > 0)
                    return (
                        '~', 
                        ConsoleColor.Red,
                        region.Terrain[Terrain.Water] == 1 ? ConsoleColor.Blue : ConsoleColor.DarkBlue);
                return (
                    ' ', 
                    ConsoleColor.White,
                    region.Terrain[Terrain.Water] == 1 ? ConsoleColor.Blue : ConsoleColor.DarkBlue);
            }

            if (region.Terrain[Terrain.Lava] > 0)
                return (' ', ConsoleColor.White, ConsoleColor.DarkRed);
            
            if (region.Terrain[Terrain.Land] <= 1)
                return (' ', ConsoleColor.White, ConsoleColor.Yellow);
            
            if (region.Terrain[Terrain.Land] <= 3)
                return (' ', ConsoleColor.White, ConsoleColor.DarkYellow);
            
            if (region.Terrain[Terrain.Land] <= 5)
                return (' ', ConsoleColor.White, ConsoleColor.Gray);
            
            return (' ', ConsoleColor.Gray, ConsoleColor.White);
        }
    }
}
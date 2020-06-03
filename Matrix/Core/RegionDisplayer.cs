using System;
using System.Linq;
using Matrix.Tools;

namespace Matrix.Core
{
    public class RegionDisplayer
    {
        public (char character, ConsoleColor foreground, ConsoleColor background) RegionToConsoleAtom(Region region)
        {
            if (region.Terrain["water"] > 0)
                return ('~', region.Terrain["lava"] > 0 ? ConsoleColor.Red : ConsoleColor.White, 
                    region.Terrain["water"] == 1 ? ConsoleColor.Blue : ConsoleColor.DarkBlue);

            if (region.Terrain["lava"] > 0)
                return ('~', ConsoleColor.White, ConsoleColor.DarkRed);
            
            if (region.Terrain["land"] <= 1)
                return (' ', ConsoleColor.White, ConsoleColor.Yellow);
            
            if (region.Terrain["land"] <= 3)
                return (' ', ConsoleColor.White, ConsoleColor.Green);
            
            if (region.Terrain["land"] <= 5)
                return (' ', ConsoleColor.White, ConsoleColor.Gray);
            
            return (' ', ConsoleColor.Gray, ConsoleColor.White);
        }
    }
}
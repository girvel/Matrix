using System;
using Matrix.Tools;
using Atom = System.ValueTuple<char, System.ConsoleColor, System.ConsoleColor>;

namespace Matrix.Core
{
    public class RegionDisplayer
    {
        public bool AreCloudsVisible = true;

        public Atom RegionToConsoleAtom(Region region, byte averageWaterHeight)
        {
            Atom GenerateBackground()
            {
                if (region.Terrain[Terrain.WATER] > 0)
                {
                    var background = ConsoleColor.DarkBlue;
                    if (region.Terrain.Water <= 4)
                    {
                        background = ConsoleColor.DarkCyan;
                    }
                    else if (region.Terrain.Water <= 7)
                    {
                        background = ConsoleColor.Blue;
                    }

                    if (region.Terrain[Terrain.LAVA] > 0)
                        return (
                            '-', 
                            ConsoleColor.Red,
                            background);

                    return (
                        ' ', 
                        ConsoleColor.White,
                        background);
                }

                if (region.Terrain[Terrain.LAVA] > 0)
                    return (' ', ConsoleColor.White, ConsoleColor.DarkRed);
            
                if (region.Terrain.Land - averageWaterHeight <= 1)
                    return (' ', ConsoleColor.DarkGray, ConsoleColor.Yellow);
            
                if (region.Terrain.Land - averageWaterHeight <= 7)
                    return (' ', ConsoleColor.DarkGray, ConsoleColor.DarkYellow);
            
                if (region.Terrain.Land - averageWaterHeight <= 10)
                    return (' ', ConsoleColor.Gray, ConsoleColor.DarkGray);
            
                return (' ', ConsoleColor.DarkGray, ConsoleColor.Gray);
            }
            
            var (ch, fg, bg) = GenerateBackground();
            
            if (ch != ' ') return (ch, fg, bg);
            
            if (region.IsRaining)
                return ('\'', ConsoleColor.White, bg);
            
            if (AreCloudsVisible && region.Terrain.Clouds > 0)
                return (
                    '#',
                    ConsoleColor.White,
                    bg);
            
            return (ch, fg, bg);
        }
    }
}
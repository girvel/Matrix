using System;
using System.Linq;
using Matrix.Tools;

namespace Matrix.Core
{
    public class RegionDisplayer
    {
        private (char character, ConsoleColor foreground, ConsoleColor background) _regionToConsoleAtom(
            Region region, 
            byte averageWaterHeight)
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
            
            if (region.Terrain.Land - averageWaterHeight <= 3)
                return (' ', ConsoleColor.DarkGray, ConsoleColor.Yellow);
            
            if (region.Terrain.Land - averageWaterHeight <= 7)
                return (' ', ConsoleColor.DarkGray, ConsoleColor.DarkYellow);
            
            if (region.Terrain.Land - averageWaterHeight <= 10)
                return (' ', ConsoleColor.Gray, ConsoleColor.DarkGray);
            
            return (' ', ConsoleColor.DarkGray, ConsoleColor.Gray);
        }

        public (char character, ConsoleColor foreground, ConsoleColor background) RegionToConsoleAtom(Region region, byte averageWaterHeight)
        {
            var result = _regionToConsoleAtom(region, averageWaterHeight);
            if (result.character == ' ')
            {
                if (region.IsRaining)
                    return ('\'', ConsoleColor.White, result.background);
                // var direction = region.FlowDirection[Terrain.WATER];
                // if (direction != Vector2.Zero)
                //     return (
                //         '~', 
                //         result.foreground, 
                //         result.background);
                if (region.Terrain.Clouds > 0)
                    return (
                        '#',
                        ConsoleColor.White,
                        result.background);
            }
            return result;
        }
    }
}
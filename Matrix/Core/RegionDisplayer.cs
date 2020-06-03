using System;

namespace Matrix.Core
{
    public class RegionDisplayer
    {
        public (char character, ConsoleColor foreground, ConsoleColor background) RegionToConsoleAtom(Region region)
        {
            return ('.', ConsoleColor.White, ConsoleColor.Gray);
        }
    }
}
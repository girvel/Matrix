using System;
using Matrix.Tools;

namespace Matrix.Core
{
    public class Session
    {
        public Field<Region> Field { get; }
        public RegionDisplayer Displayer { get; }

        public Session()
        {
            Field = new Field<Region>(new Vector2(169, 35), v => new Region());
            Displayer = new RegionDisplayer();
        }

        public void Start()
        {
            Console.SetWindowSize(Field.Size.X + 1, Field.Size.Y + 1);
            Console.SetBufferSize(Field.Size.X + 1, Field.Size.Y + 1);
            Console.CursorVisible = false;
            Console.Title = "Matrix";

            while (true)
            {
                Display();
            }
        }

        public void Display()
        {
            var startingTime = DateTime.Now;
            Console.SetCursorPosition(0, 0);

            var line = "";
            foreach (var (v, region) in Field)
            {
                var atom = Displayer.RegionToConsoleAtom(region);

                if (atom.background != Console.BackgroundColor || atom.foreground != Console.ForegroundColor)
                {
                    Console.Write(line);
                    Console.CursorVisible = false;
                    line = "";
                    Console.BackgroundColor = atom.background;
                    Console.ForegroundColor = atom.foreground;
                }
                    
                line += (atom.character);

                if (v.X == Field.Size.X - 1)
                {
                    line += "\n";
                }
            }
                
            Console.Write(line);
            Console.CursorVisible = false;

            var frameTime = DateTime.Now - startingTime;
            Console.ResetColor();
            Console.Write($"UI FPS: {1 / frameTime.TotalSeconds:F}");
        }
    }
}
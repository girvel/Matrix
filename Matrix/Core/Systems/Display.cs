using System;
using Matrix.Tools;

namespace Matrix.Core.Systems
{
    public class Display : Core.System
    {
        public RegionDisplayer Displayer { get; } = new RegionDisplayer();
        
        private void ShowData(string name, object value)
        {
            Console.Write($"{name}: ");
            switch (value)
            {
                case float f:
                    Console.Write(f.ToString("F"));
                    break;
                    
                case double d:
                    Console.Write(d.ToString("F"));
                    break;
                    
                default:
                    Console.Write(value);
                    break;
            }
            Console.Write("\t");
        }
        
        public override void Update()
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

            var line = "";
            foreach (var (v, region) in Session.Field)
            {
                var atom = Displayer.RegionToConsoleAtom(region, Session.AverageWaterHeight);

                if (atom.background != Console.BackgroundColor || atom.foreground != Console.ForegroundColor)
                {
                    Console.Write(line);
                    Console.CursorVisible = false;
                    line = "";
                    Console.BackgroundColor = atom.background;
                    Console.ForegroundColor = atom.foreground;
                }
                    
                line += atom.character;

                if (v.X == Session.Field.Size.X - 1)
                {
                    line += "\n";
                }
            }
                
            Console.Write(line);
            Console.CursorVisible = false;
            
            Console.ResetColor();
            
            ShowData("AVG UI FQ", 1 / Clocks.ResumeData("UI"));
            ShowData("AVG SYSTEMS FQ", 1 / Clocks.ResumeData("SYSTEMS"));
            ShowData("FIELD SIZE", Session.Field.Size);
            ShowData("Date", Session.CurrentDate);
        }
    }
}
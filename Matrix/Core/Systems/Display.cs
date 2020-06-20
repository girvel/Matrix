using System;
using System.Linq;
using Matrix.Tools;

namespace Matrix.Core.Systems
{
    public class Display : Core.System
    {
        public Display(RegionDisplayer displayer, Action cancelKeyAction)
        {
            Displayer = displayer;
            Console.CursorVisible = false;
            Console.Title = "Matrix";
            
#if DEBUG
            Rarity = 30;
#endif

            Console.CancelKeyPress += (o, args) => cancelKeyAction();
        }
        
        public RegionDisplayer Displayer { get; }
        
        public byte AverageWaterHeight;
        
        private void ShowData(string name, object value, string unitOfMeasure=null)
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

            if (unitOfMeasure != null)
            {
                Console.Write(" " + unitOfMeasure);
            }
            
            Console.Write("\t");
        }
        
        protected override void _update()
        {
            AverageWaterHeight = (byte) State.Field
                .Where(t => t.content.Terrain.Water != 0)
                .Average(t => t.content.Terrain.SliceFrom(Terrain.WATER));
            
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

            var line = "";
            foreach (var (v, region) in State.Field)
            {
                var (character, foreground, background) 
                    = Displayer.RegionToConsoleAtom(region, AverageWaterHeight);

                if (background != Console.BackgroundColor || foreground != Console.ForegroundColor)
                {
                    Console.Write(line);
                    Console.CursorVisible = false;
                    line = "";
                    Console.BackgroundColor = background;
                    Console.ForegroundColor = foreground;
                }
                    
                line += character;

                if (v.X == State.Field.Size.X - 1)
                {
                    line += "\n";
                }
            }
                
            Console.Write(line);
            Console.CursorVisible = false;
            
            Console.ResetColor();
            
            ShowData("Fq", 1 / Clocks.ResumeData("SYSTEMS FQ"));
            ShowData("Size", State.Field.Size.Area / 0.4047, "acre");
            ShowData("Date", State.Date);
        }
    }
}
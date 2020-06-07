using System;
using System.Linq;
using Matrix.Core.Systems;
using Matrix.Tools;

namespace Matrix.Core
{
    public class Session
    {
        public Field<Region> Field { get; private set; }
        public Random Random { get; private set; }
        public System[] Systems;
        
        public DateTime CurrentDate = new DateTime(1, 1, 1);
        public byte AverageWaterHeight;



        public void Start()
        {
            Console.CursorVisible = false;
            Console.Title = "Matrix";

            Systems = new System[]
            {
                new Display(), 
                new Volcanoes(), 
                new LavaToLand(), 
                new Flow(),
                new Wind(),
                new Rain(),
                new Evaporation(), 
                new Cleaning(), 
            };

            foreach (var s in Systems)
            {
                s.Session = this;
            }
            
            Console.Write("Seed: @");
            Random = new Random(Console.ReadLine()?.GetHashCode() ?? 0);
            Field = new Field<Region>(
                new Vector2(Console.WindowWidth - 1, Console.WindowHeight - 5), 
                v => new Region());
            AverageWaterHeight = (byte) Field.Average(t => t.t.Terrain.Water);

            while (true)
            {
                using (new Clocks.Timer("UI"))
                    Systems[0].Update();

                using (new Clocks.Timer("SYSTEMS"))
                {
                    for (var i = 1; i < Systems.Length; i++)
                    {
                        Systems[i].Update();
                    }

                    CurrentDate += TimeSpan.FromDays(1);
                }
            }
        }
    }
}
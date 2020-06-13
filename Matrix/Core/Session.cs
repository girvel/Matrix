using System;
using System.IO;
using System.Linq;
using Matrix.Core.Systems;
using Matrix.Tools;
using Newtonsoft.Json;

namespace Matrix.Core
{
    public class Session
    {
        public Field<Region> Field { get; private set; }
        public volatile Random Random;
        public System[] Systems;
        
        public DateTime CurrentDate = new DateTime(1, 1, 1);
        public byte AverageWaterHeight;



        public void Start()
        {
            Console.CursorVisible = false;
            Console.Title = "Matrix";

            Systems = new Core.System[]
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
            
            Field = new WorldFactory().Produce(
                new int2(Console.WindowWidth - 1, Console.WindowHeight - 5), 
                Random);
            
            AverageWaterHeight = (byte) Field.Average(t => t.content.Terrain.Water);

            Console.CancelKeyPress += (o, args) =>
            {
                var directory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Matrix");

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using var file =
                    File.OpenWrite(Path.Combine(directory, $"Statistics of {DateTime.Now:dd.MM.yyyy HH-mm-ss}.json"));
                using var writer = new StreamWriter(file);
                writer.Write(JsonConvert.SerializeObject(System.GetFrequency(Systems), Formatting.Indented));
            };

            while (true)
            {
                using (new Clocks.Timer("FPS"))
                {
                    foreach (var s in Systems)
                    {
                        s.Update();
                    }

                    CurrentDate += TimeSpan.FromDays(1);
                }
            }
        }
    }
}
using System;
using System.IO;
using System.Linq;
using Angem;
using Matrix.Core.Systems;
using Matrix.Tools;
using Newtonsoft.Json;

namespace Matrix.Core
{
    public class Session
    {
        public System[] Systems;

        public State State;

        public void Start()
        {
            State = new WorldFactory().Produce(
                new int2(Console.WindowWidth - 1, Console.WindowHeight - 5));

            void SaveStatistics()
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
            }

            Systems = new System[]
            {
                new Display(SaveStatistics), 
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
                s.State = State;
            }

            while (true)
            {
                using (new Clocks.Timer("SYSTEMS FQ"))
                {
                    foreach (var s in Systems)
                    {
                        s.Update();
                    }

                    State.Date += TimeSpan.FromDays(1);
                }
            }
        }
    }
}
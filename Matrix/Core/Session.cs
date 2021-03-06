﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Angem;
using Matrix.Core.Systems;
using Matrix.Core.Systems.Input;
using Matrix.Tools;
using Newtonsoft.Json;

namespace Matrix.Core
{
    public class Session
    {
        public System[] Systems;

        public System InputSystem;

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

            var displayer = new RegionDisplayer();
            
            Systems = new System[]
            {
                new Display(displayer, SaveStatistics), 
                new Volcanoes(), 
                new LavaToLand(), 
                new Flow(),
                new Pressure(),
                new Wind(),
                new Rain(),
                new Evaporation(), 
                new Cleaning(), 
            };

            foreach (var s in Systems)
            {
                s.State = State;
            }

            InputSystem = new Input(displayer);
            new Thread(() =>
            {
                while (true) InputSystem.Update();
            }).Start();
            InputSystem.State = State;

            while (true)
            {
                using (new Clocks.Timer("SYSTEMS FQ"))
                {
                    // State.NextField = State.Field.DeepClone();
                        
                    foreach (var s in Systems)
                    {
                        s.Update();
                    }

                    State.Date += TimeSpan.FromDays(1);
                    // State.Field = State.NextField;
                }
            }
        }
    }
}
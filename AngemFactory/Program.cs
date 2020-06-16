using System;
using System.IO;

namespace AngemFactory
{
    internal static class Program
    {
        public static readonly Factory Factory = new Factory
        {
            Types = new[] {"int", "float"},
            Sizes = new[] {2, 3},
            CommonTemplate = File.ReadAllText("../../Templates/Common.txt"),
            ContentTemplates =
            {
                [2] = File.ReadAllText("../../Templates/2D.txt"),
                [3] = File.ReadAllText("../../Templates/3D.txt"),
            },
            LibraryName = "Angem",
            ConversionTemplates =
            {
                [2] = File.ReadAllText("../../Templates/Conversion2D.txt"),
                [3] = File.ReadAllText("../../Templates/Conversion3D.txt"),
            },
            Conversions =
            {
                ["int"] = new[] {(false, "float")},
                ["float"] = new[] {(true, "int")},
            }
        };

        public static readonly string LibraryRoot = "../../../Angem";
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Generation started");
            foreach (var (name, content) in Factory.Generate())
            {
                using var file = File.OpenWrite(Path.Combine(LibraryRoot, name + ".cs"));
                using var stream = new StreamWriter(file);
                
                stream.Write(content);
                Console.WriteLine($"Generated {name}.cs");
            }

            Console.WriteLine("Generation ended");
        }
    }
}
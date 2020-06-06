using System;
using System.Linq;
using Matrix.Tools;

namespace Matrix.Core
{
    public class Session
    {
        public Field<Region> Field { get; private set; }
        public RegionDisplayer Displayer { get; }
        public Random Random { get; private set; }
        public DateTime CurrentDate = new DateTime(1, 1, 1);
        public byte AverageWaterHeight;



        private static readonly Vector2[]
            LandMakingDirections =
            {
                Vector2.Zero, Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left,
            },
            FlowDirections =
            {
                Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left,
            },
            WindDirections =
            {
                Vector2.Right,
            },
            GasAdditionalDirections =
            {
                Vector2.Up,
                Vector2.Down,
            };



        public Session()
        {
            Displayer = new RegionDisplayer();
        }

        public void Start()
        {
            Console.CursorVisible = false;
            Console.Title = "Matrix";

            Console.Write("Seed: @");
            Random = new Random(Console.ReadLine()?.GetHashCode() ?? 0);
            Field = new Field<Region>(
                new Vector2(Console.WindowWidth - 1, Console.WindowHeight - 5), 
                v => new Region());
            AverageWaterHeight = (byte) Field.Average(t => t.t.Terrain.Water);

            while (true)
            {
                using (new Clocks.Timer("UI"))
                    Display();

                using (new Clocks.Timer("SYSTEMS"))
                {
                    for (var i = 0; i < 30; i++)
                    {
                        GenerateLavaActivity();
                        MakeLandFromLava();
                        MoveFluid(Terrain.LAVA, 0.9);
                        MoveFluid(Terrain.LAND, 0.01);
                        MoveFluid(Terrain.CLOUDS, 0.95);
                        MoveFluid(Terrain.WATER, 0.95);
                        MoveGas(Terrain.CLOUDS, 0.95, 0.3);
                        MakeItRain(0.8);
                        VaporizeWater(0.001);
                        RemoveBottomLayers();

                        CurrentDate += TimeSpan.FromDays(1);
                    }
                }
            }
        }

        public void Display()
        {
            void ShowData(string name, object value)
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
            
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

            var line = "";
            foreach (var (v, region) in Field)
            {
                var atom = Displayer.RegionToConsoleAtom(region, AverageWaterHeight);

                if (atom.background != Console.BackgroundColor || atom.foreground != Console.ForegroundColor)
                {
                    Console.Write(line);
                    Console.CursorVisible = false;
                    line = "";
                    Console.BackgroundColor = atom.background;
                    Console.ForegroundColor = atom.foreground;
                }
                    
                line += atom.character;

                if (v.X == Field.Size.X - 1)
                {
                    line += "\n";
                }
            }
                
            Console.Write(line);
            Console.CursorVisible = false;
            
            Console.ResetColor();
            
            ShowData("AVG UI FQ", 1 / Clocks.ResumeData("UI"));
            ShowData("AVG SYSTEMS FQ", 1 / Clocks.ResumeData("SYSTEMS"));
            ShowData("FIELD SIZE", Field.Size);
            ShowData("Date", CurrentDate);
        }

        public void GenerateLavaActivity()
        {
            foreach (var (v, region) in Field)
            {
                if (Random.NextDouble() < 0.00005)
                {
                    region.Terrain.Lava += region.LavaPotential;
                }
            }
        }
        
        public void MakeLandFromLava() // TODO: depending on temperature
        {
            foreach (var (v, region) in Field)
            {
                if (Random.NextDouble() >= 0.95) continue;

                foreach (var dir in LandMakingDirections)
                {
                    if (region.Terrain[Terrain.LAVA] <= 0) break;
                    if (!(v + dir).Inside(Field.Size)) continue;
                    
                    var other = Field[v + dir];
                    
                    if (other.Terrain[Terrain.WATER] <= 0) continue;
                    
                    region.Terrain[Terrain.LAND]++;
                    region.Terrain[Terrain.LAVA]--;
                    other.Terrain[Terrain.WATER]--;
                    other.Terrain[Terrain.CLOUDS]++;
                    break;
                }

                if (region.Terrain[Terrain.LAVA] > 0 && Random.NextDouble() < 0.1)
                {
                    region.Terrain[Terrain.LAND]++;
                    region.Terrain[Terrain.LAVA]--;
                }
            }
        }

        public void MoveFluid(byte fluid, double chance)
        {
            foreach (var (v, region) in Field)
            {
                region.FlowDirection[fluid] = Vector2.Zero;
                foreach (var dir in FlowDirections)
                {
                    if (region.Terrain[fluid] == 0) break;
                    if (!(v + dir).Inside(Field.Size)) continue;

                    var other = Field[v + dir];
                    
                    var regionSum = region.Terrain.SliceFrom(fluid);
                    var otherSum = other.Terrain.SliceFrom(fluid);
                    
                    if (regionSum - otherSum > 1 && Random.NextDouble() < chance
                        || regionSum - otherSum == 1 && Random.NextDouble() < chance / 2)
                    {
                        other.Terrain[fluid]++;
                        region.Terrain[fluid]--;
                        region.FlowDirection[fluid] = dir;
                    }
                }
            }
        }

        public void MoveGas(byte gas, double maximalChance, double maximalAdditionalChance)
        {
            foreach (var (v, region) in Field)
            {
                foreach (var dir in WindDirections)
                {
                    if (!(v + dir).Inside(Field.Size)) continue;

                    var other = Field[v + dir];
                    if (Random.NextDouble() >=
                        maximalChance * Math.Pow(
                            1.41, 
                            region.Terrain.SliceFrom(Terrain.CLOUDS)
                            - other.Terrain.SliceFrom(Terrain.CLOUDS) - 2))  // TODO: precalc
                        continue;

                    other.Terrain.Clouds += region.Terrain.Clouds;
                    region.Terrain.Clouds = 0;
                    break;
                }

                foreach (var dir in GasAdditionalDirections)
                {
                    if (!(v + dir).Inside(Field.Size)) continue;

                    var other = Field[v + dir];
                    if (Random.NextDouble() >=
                        maximalAdditionalChance * Math.Pow(1.41, region.Terrain.Clouds - other.Terrain.SliceFrom())) continue; // TODO: precalc

                    other.Terrain.Clouds += region.Terrain.Clouds;
                    region.Terrain.Clouds = 0;
                    break;
                }
            }
        }

        public void MakeItRain(double maximalChance)
        {
            foreach (var (v, region) in Field)
            {
                region.IsRaining = Random.NextDouble() < maximalChance * (1 - Math.Pow(1.054, -region.Terrain.Clouds)); // TODO: precalc
                if (!region.IsRaining) continue;
                region.Terrain.Clouds--;
                region.Terrain.Water++;
            }
        }

        public void VaporizeWater(double chance)
        {
            foreach (var (v, region) in Field)
            {
                if (region.Terrain.Water <= 0 || !(Random.NextDouble() < chance)) continue;
                region.Terrain.Water--;
                region.Terrain.Clouds++;
            }
        }

        public void RemoveBottomLayers()
        {
            var min = Field.Min(t => t.t.Terrain.Land);
            foreach (var (v, region) in Field)
            {
                region.Terrain.Land -= min;
            }
        }
    }
}
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



        private static readonly Vector2[]
            LandMakingDirections =
            {
                Vector2.Zero, Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left,
            },
            FlowDirections =
            {
                Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left,
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

            while (true)
            {
                using (new Clocks.Timer("UI"))
                    Display();

                using (new Clocks.Timer("SYSTEMS"))
                {
                    GenerateLavaActivity();
                    MakeLandFromLava();
                    MoveFluid(Terrain.Lava, 0.6, 0.01);
                    MoveFluid(Terrain.Water, 0.95, 0.02);
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
                var atom = Displayer.RegionToConsoleAtom(region);

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
        }

        public void GenerateLavaActivity()
        {
            foreach (var (v, region) in Field)
            {
                if (Random.NextDouble() < 0.0001 * Math.Exp(-region.Terrain[Terrain.Land]))
                {
                    region.Terrain[Terrain.Lava] += region.LavaPotential;
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
                    if (region.Terrain[Terrain.Lava] <= 0) break;
                    if (!(v + dir).Inside(Field.Size)) continue;
                    
                    var other = Field[v + dir];
                    
                    if (other.Terrain[Terrain.Water] <= 0) continue;
                    
                    region.Terrain[Terrain.Land]++;
                    region.Terrain[Terrain.Lava]--;
                    other.Terrain[Terrain.Water]--; // TODO: humidity
                    break;
                }

                if (region.Terrain[Terrain.Lava] > 0 && Random.NextDouble() < 0.1)
                {
                    region.Terrain[Terrain.Land]++;
                    region.Terrain[Terrain.Lava]--;
                }
            }
        }

        private void MoveFluid(byte fluid, double chance, double waveChance)
        {
            foreach (var (v, region) in Field)
            {
                foreach (var dir in FlowDirections)
                {
                    if (region.Terrain[fluid] == 0) break;
                    if (!(v + dir).Inside(Field.Size)) continue;

                    var other = Field[v + dir];
                    
                    var regionSum = region.Terrain.SliceFrom(fluid);
                    var otherSum = other.Terrain.SliceFrom(fluid);
                    
                    if (regionSum - otherSum > 1 && Random.NextDouble() < chance
                        || regionSum - otherSum == 1 && Random.NextDouble() < waveChance)
                    {
                        other.Terrain[fluid]++;
                        region.Terrain[fluid]--;
                    }
                }
            }
        }
    }
}
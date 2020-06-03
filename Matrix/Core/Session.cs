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

        public TimeSpan CalculationsFrameTime = TimeSpan.Zero;



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
                Display();

                var start = DateTime.Now;
                
                GenerateLavaActivity();
                MakeLandFromLava();
                MoveFluid("lava", 0.6, 0.01);
                MoveFluid("water", 0.95, 0.02);
                
                CalculationsFrameTime = DateTime.Now - start;
            }
        }

        public void Display()
        {
            var startingTime = DateTime.Now;
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

            var frameTime = DateTime.Now - startingTime;
            Console.ResetColor();
            Console.Write(
                string.Format(
                    "UI FPS: {0:F}\t SYSTEMS' FPS: {1}", 
                    1 / frameTime.TotalSeconds,
                    1 / CalculationsFrameTime.TotalSeconds));
        }

        public void GenerateLavaActivity()
        {
            foreach (var (v, region) in Field)
            {
                if (Random.NextDouble() < 0.0001 * Math.Exp(-region.Terrain["land"]))
                {
                    region.Terrain["lava"] += region.LavaPotential;
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
                    if (region.Terrain["lava"] <= 0) break;
                    if (!(v + dir).Inside(Field.Size)) continue;
                    
                    var other = Field[v + dir];
                    
                    if (other.Terrain["water"] <= 0) continue;
                    
                    region.Terrain["land"]++;
                    region.Terrain["lava"]--;
                    other.Terrain["water"]--; // TODO: humidity
                    break;
                }

                if (region.Terrain["lava"] > 0 && Random.NextDouble() < 0.1)
                {
                    region.Terrain["land"]++;
                    region.Terrain["lava"]--;
                }
            }
        }

        private void MoveFluid(string fluid, double chance, double waveChance)
        {
            foreach (var (v, region) in Field)
            {
                foreach (var dir in FlowDirections)
                {
                    if (region.Terrain[fluid] == 0) break;
                    if (!(v + dir).Inside(Field.Size)) continue;

                    var other = Field[v + dir];
                    
                    var region_sum = region.Terrain.SliceFrom(fluid).Sum();
                    var other_sum = other.Terrain.SliceFrom(fluid).Sum();
                    
                    if (region_sum - other_sum > 1 && Random.NextDouble() < chance
                        || region_sum - other_sum == 1 && Random.NextDouble() < waveChance)
                    {
                        other.Terrain[fluid]++;
                        region.Terrain[fluid]--;
                    }
                }
            }
        }
    }
}
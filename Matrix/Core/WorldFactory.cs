using System;
using Angem;
using Matrix.Tools;

namespace Matrix.Core
{
    public class WorldFactory
    {
        [Constant] public byte DefaultLavaPotential;

        [Constant] public double RiftsDensity;

        [Constant] public int MinRiftLineLength, MaxRiftLineLength;



        public WorldFactory()
        {
            ConstantAttribute.UpdateConstants(this);
        }
        
        public State Produce(int2 size)
        {
            var result = new State {Field = new Field<Region>(size, v => new Region())};
            
            Console.Write("Seed: @");
            result.Random = new Random(Console.ReadLine()?.GetHashCode() ?? 0);

            Console.WriteLine("Rift generation begins");

            var startingPoint = float2.Zero;
            for (var _ = 0; _ < (size.X + size.Y) * RiftsDensity; _++)
            {
                startingPoint = new float2(
                    (startingPoint.X + result.Random.Next(size.X / 3, size.X * 2 / 3)) % size.X, 
                    0);
                
                var point = startingPoint;

                Console.Write(point);
                
                var delta = float2.Back;
                var maxLineLength = 0;
                var currentLineLength = 0;
            
                while (((int2) point).Inside(size))
                {
                    result.Field[(int2) point].LavaPotential += DefaultLavaPotential;
                    
                    if (currentLineLength >= maxLineLength)
                    {
                        delta = delta.Rotated(result.Random.NextDouble(-45, 45));
                        maxLineLength = result.Random.Next(MinRiftLineLength, MaxRiftLineLength);
                        currentLineLength = 0;
                    }
                    
                    point += delta;
                    currentLineLength++;
                }
                
                Console.WriteLine($"  Rift #{_} is generated");
            }
            Console.WriteLine("Rift generation ends");
            Console.ReadKey(true);
            
            return result;
        }
    }
}
using System;
using Matrix.Tools;

namespace Matrix.Core
{
    public class WorldFactory
    {
        public byte DefaultLavaPotential = 50;

        public double RiftsDensity = 0.027;

        public int MinRiftLineLength = 10, MaxRiftLineLength = 20;
        
        
        
        public Field<Region> Produce(int2 size, Random random)
        {
            var result = new Field<Region>(size, v => new Region());

            Console.WriteLine("Rift generation begins");

            var startingPoint = float2.Zero;
            for (var _ = 0; _ < (size.X + size.Y) * RiftsDensity; _++)
            {
                startingPoint = new float2((startingPoint.X + random.Next(size.X / 3, size.X * 2 / 3)) % size.X, 0);
                var point = startingPoint;

                Console.Write(point);
                
                var delta = float2.Down;
                var maxLineLength = 0;
                var currentLineLength = 0;
            
                while (((int2) point).Inside(size))
                {
                    result[(int2) point].LavaPotential += DefaultLavaPotential;
                    
                    if (currentLineLength >= maxLineLength)
                    {
                        delta = delta.Rotated(random.NextDouble(-45, 45));
                        maxLineLength = random.Next(MinRiftLineLength, MaxRiftLineLength);
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
using System;
using Matrix.Tools;

namespace Matrix.Core
{
    public class WorldFactory
    {
        public byte DefaultLavaPotential = 50;
        
        public Field<Region> Produce(int2 size, Random random)
        {
            var result = new Field<Region>(size, v => new Region());

            for (var _ = 0; _ < 3; _++)
            {
                var v = new float2(random.Next(size.X), 0);
                var delta = float2.Down.Rotated(random.NextDouble() * 60 - 30);
            
                while (((int2) v).Inside(size))
                {
                    v += delta;
                    result[(int2) v].LavaPotential += DefaultLavaPotential;
                }
            }
            
            return result;
        }
    }
}
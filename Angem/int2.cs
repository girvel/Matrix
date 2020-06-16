using System;

namespace Angem
{
    public readonly struct int2
    {
        public static readonly int2
            Zero = new int2(0, 0),
            Forward = new int2(0, -1),
            Back = new int2(0, 1),
            Right = new int2(1, 0),
            Left = new int2(-1, 0);
            
        public readonly int X, Y;
        public readonly int Area;
        
        public int2(int x, int y)
        {
            X = x;
            Y = y;
            Area = X * Y;
        }
        
        public static int2 operator +(int2 v1, int2 v2)
        {
            return new int2(
                v1.X + v2.X,
                v1.Y + v2.Y);
        }
 
        public static bool operator <(int2 v, int2 size)
        {
            return v.X < size.X && v.Y < size.Y;
        }
 
        public static bool operator <=(int2 v, int2 size)
        {
            return v.X <= size.X && v.Y <= size.Y;
        }
        

        public static implicit operator float2(int2 original) 
            => new float2(
                (float) original.X, 
                (float) original.Y);
 
        public override string ToString() => $"{{{X}; {Y}}}";
 
        public static bool operator ==(int2 v, int2 u) => v.X == u.X && v.Y == u.Y;
 
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
        
        public int2 Rotated(double degrees)
        {
            var angle = degrees / 180 * Math.PI;
            double sin = Math.Sin(angle), cos = Math.Cos(angle);
            return new int2(
                (int) (X * cos - Y * sin),
                (int) (X * sin + Y * cos));
        }
        
    
    
        public bool Inside(int2 max) => Zero <= this && this < max;
        
        public bool Inside(int2 min, int2 max) => min <= this && this < max;
        
        public override bool Equals(object obj) => obj is int2 other && this == other;
        
        public static bool operator !=(int2 v, int2 u) => !(v == u);
 
        public static bool operator >(int2 size, int2 v) => v < size;
 
        public static bool operator >=(int2 size, int2 v) => v <= size;
    }
}
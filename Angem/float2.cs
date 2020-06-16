using System;

namespace Angem
{
    public readonly struct float2
    {
        public static readonly float2
            Zero = new float2(0, 0),
            Forward = new float2(0, -1),
            Back = new float2(0, 1),
            Right = new float2(1, 0),
            Left = new float2(-1, 0);
            
        public readonly float X, Y;
        public readonly float Area;
        
        public float2(float x, float y)
        {
            X = x;
            Y = y;
            Area = X * Y;
        }
        
        public static float2 operator +(float2 v1, float2 v2)
        {
            return new float2(
                v1.X + v2.X,
                v1.Y + v2.Y);
        }
 
        public static bool operator <(float2 v, float2 size)
        {
            return v.X < size.X && v.Y < size.Y;
        }
 
        public static bool operator <=(float2 v, float2 size)
        {
            return v.X <= size.X && v.Y <= size.Y;
        }
        

        public static explicit operator int2(float2 original) 
            => new int2(
                (int) original.X, 
                (int) original.Y);
 
        public override string ToString() => $"{{{X}; {Y}}}";
 
        public static bool operator ==(float2 v, float2 u) => v.X == u.X && v.Y == u.Y;
 
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
        
        public float2 Rotated(double degrees)
        {
            var angle = degrees / 180 * Math.PI;
            double sin = Math.Sin(angle), cos = Math.Cos(angle);
            return new float2(
                (float) (X * cos - Y * sin),
                (float) (X * sin + Y * cos));
        }
        
    
    
        public bool Inside(float2 max) => Zero <= this && this < max;
        
        public bool Inside(float2 min, float2 max) => min <= this && this < max;
        
        public override bool Equals(object obj) => obj is float2 other && this == other;
        
        public static bool operator !=(float2 v, float2 u) => !(v == u);
 
        public static bool operator >(float2 size, float2 v) => v < size;
 
        public static bool operator >=(float2 size, float2 v) => v <= size;
    }
}
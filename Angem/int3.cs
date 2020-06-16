using System;

namespace Angem
{
    public readonly struct int3
    {
        public static readonly int3
            Zero = new int3(0, 0, 0),
            Forward = new int3(0, -1, 0),
            Back = new int3(0, 1, 0),
            Right = new int3(1, 0, 0),
            Left = new int3(-1, 0, 0),
            Up = new int3(0, 0, 1),
            Down = new int3(0, 0, -1);
            
        public readonly int X, Y, Z;
        public readonly int Volume;
        
        public int3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Volume = X * Y * Z;
        }
        
        public static int3 operator +(int3 v1, int3 v2)
        {
            return new int3(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z);
        }
        
        public static int3 operator -(int3 v1, int3 v2)
        {
            return new int3(
                v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z);
        }
        
        public static int3 operator -(int3 v)
        {
            return new int3(-v.X, -v.Y, -v.Z);
        }
        
        public static int operator *(int3 v1, int3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
 
        public static bool operator <(int3 v, int3 size)
        {
            return v.X < size.X && v.Y < size.Y && v.Z < size.Z;
        }
 
        public static bool operator <=(int3 v, int3 size)
        {
            return v.X <= size.X && v.Y <= size.Y && v.Z <= size.Z;
        }
        

        public static implicit operator float3(int3 original) 
            => new float3(
                (float) original.X, 
                (float) original.Y, 
                (float) original.Z);
 
        public override string ToString() => $"{{{X:F}; {Y:F}; {Z:F} }}";
 
        public static bool operator ==(int3 v, int3 u) => v.X == u.X && v.Y == u.Y && v.Z == u.Z;
 
        public override int GetHashCode()
        {
            unchecked
            {
                return ((X.GetHashCode() * 397) ^ Y.GetHashCode() * 397) ^ Z.GetHashCode();
            }
        }
        
    
    
        public bool Inside(int3 max) => Zero <= this && this < max;
        
        public bool Inside(int3 min, int3 max) => min <= this && this < max;
        
        public override bool Equals(object obj) => obj is int3 other && this == other;
        
        public static bool operator !=(int3 v, int3 u) => !(v == u);
 
        public static bool operator >(int3 size, int3 v) => v < size;
 
        public static bool operator >=(int3 size, int3 v) => v <= size;
    }
}
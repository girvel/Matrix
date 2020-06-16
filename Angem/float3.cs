using System;

namespace Angem
{
    public readonly struct float3
    {
        public static readonly float3
            Zero = new float3(0, 0, 0),
            Forward = new float3(0, -1, 0),
            Back = new float3(0, 1, 0),
            Right = new float3(1, 0, 0),
            Left = new float3(-1, 0, 0),
            Up = new float3(0, 0, 1),
            Down = new float3(0, 0, -1);
            
        public readonly float X, Y, Z;
        public readonly float Area;
        
        public float3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            Area = X * Y;
        }
        
        public static float3 operator +(float3 v1, float3 v2)
        {
            return new float3(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z);
        }
 
        public static bool operator <(float3 v, float3 size)
        {
            return v.X < size.X && v.Y < size.Y && v.Z < size.Z;
        }
 
        public static bool operator <=(float3 v, float3 size)
        {
            return v.X <= size.X && v.Y <= size.Y && v.Z <= size.Z;
        }
        

        public static explicit operator int3(float3 original) 
            => new int3(
                (int) original.X, 
                (int) original.Y, 
                (int) original.Z);
 
        public override string ToString() => $"{{{X}; {Y}; {Z}}}";
 
        public static bool operator ==(float3 v, float3 u) => v.X == u.X && v.Y == u.Y && v.Z == v.Z;
 
        public override int GetHashCode()
        {
            unchecked
            {
                return ((X.GetHashCode() * 397) ^ Y.GetHashCode() * 397) ^ Z.GetHashCode();
            }
        }
        
    
    
        public bool Inside(float3 max) => Zero <= this && this < max;
        
        public bool Inside(float3 min, float3 max) => min <= this && this < max;
        
        public override bool Equals(object obj) => obj is float3 other && this == other;
        
        public static bool operator !=(float3 v, float3 u) => !(v == u);
 
        public static bool operator >(float3 size, float3 v) => v < size;
 
        public static bool operator >=(float3 size, float3 v) => v <= size;
    }
}
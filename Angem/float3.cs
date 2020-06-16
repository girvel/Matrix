using System;
using System.Collections.Generic;

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
        public readonly float Volume;
        
        public float3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            Volume = X * Y * Z;
        }
        
        public static float3 operator +(float3 v1, float3 v2)
        {
            return new float3(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z);
        }
        
        public static float3 operator -(float3 v1, float3 v2)
        {
            return new float3(
                v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z);
        }
        
        public static float3 operator -(float3 v)
        {
            return new float3(-v.X, -v.Y, -v.Z);
        }
        
        public static float3 operator *(float k, float3 v)
        {
            return new float3(k * v.X, k * v.Y, k * v.Z);
        }
        
        public static float operator *(float3 v1, float3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
 
        public static bool operator <(float3 v, float3 size)
        {
            return v.X < size.X && v.Y < size.Y && v.Z < size.Z;
        }
 
        public static bool operator <=(float3 v, float3 size)
        {
            return v.X <= size.X && v.Y <= size.Y && v.Z <= size.Z;
        }
        
        public static explicit operator float2(float3 original) => new float2(original.X, original.Y);
        

        public static explicit operator int3(float3 original) 
            => new int3(
                (int) original.X, 
                (int) original.Y, 
                (int) original.Z);
 
        public override string ToString() => $"{{{X:F}; {Y:F}; {Z:F} }}";
 
        public static bool operator ==(float3 v, float3 u) => v.X == u.X && v.Y == u.Y && v.Z == u.Z;
 
        public override int GetHashCode()
        {
            unchecked
            {
                return ((X.GetHashCode() * 397) ^ Y.GetHashCode() * 397) ^ Z.GetHashCode();
            }
        }
        
        public IEnumerable<float3> Range()
        {
            for (var z = 0; z < Z; z++)
            for (var y = 0; y < Y; y++)
            for (var x = 0; x < X; x++)
                    yield return new float3(x, y, z);
        }
        
    
    
        public bool Inside(float3 max) => Zero <= this && this < max;
        
        public bool Inside(float3 min, float3 max) => min <= this && this < max;
        
        public override bool Equals(object obj) => obj is float3 other && this == other;
        
        public static bool operator !=(float3 v, float3 u) => !(v == u);
 
        public static bool operator >(float3 size, float3 v) => v < size;
 
        public static bool operator >=(float3 size, float3 v) => v <= size;
        
        public static float3 operator *(float3 v, float k) => k * v;
    }
}
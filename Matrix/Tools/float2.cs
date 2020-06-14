using System;

namespace Matrix.Tools
{
    public readonly struct float2
    {
        public static readonly float2
            Zero = new float2(0, 0),
            Up = new float2(0, -1),
            Right = new float2(1, 0),
            Down = new float2(0, 1),
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
        
        public static float2 operator +(float2 v1, int2 v2)
        {
            return new float2(
                v1.X + v2.X,
                v1.Y + v2.Y);
        }

        public static float2 operator +(int2 v1, float2 v2) => v2 + v1;
        
        public static float2 operator %(float2 v1, int2 v2)
            => new float2(v1.X % v2.X, v1.Y % v2.Y);

        public static explicit operator int2(float2 v)
        {
            return new int2((int) v.X, (int) v.Y);
        }

        public override string ToString() => $"{{{X:F}; {Y.ToString("F")}}}";

        public float2 Rotated(double angle)
        {
            angle *= Math.PI / 180;
            var (sin, cos) = ((float) Math.Sin(angle), (float) Math.Cos(angle));

            return new float2(
                X * cos - Y * sin,
                X * sin + Y * cos);
        }
    }
}
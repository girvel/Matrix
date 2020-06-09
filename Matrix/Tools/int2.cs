using System.Collections.Specialized;

namespace Matrix.Tools
{
    public readonly struct int2
    {
        public static readonly int2
            Zero = new int2(0, 0),
            Up = new int2(0, -1),
            Right = new int2(1, 0),
            Down = new int2(0, 1),
            Left = new int2(-1, 0);
        
        
        public readonly int X, Y;

        public int2(int x, int y)
        {
            X = x;
            Y = y;
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

        public static bool operator >(int2 size, int2 v) => v < size;

        public static bool operator <=(int2 v, int2 size)
        {
            return v.X <= size.X && v.Y <= size.Y;
        }

        public static bool operator >=(int2 size, int2 v) => v <= size;

        public bool Inside(int2 a, int2? b = null)
        {
            return b == null 
                ? Zero <= this && this < a 
                : a <= this && this < b;
        }

        public override string ToString() => $"{{{X}; {Y}}}";

        public static bool operator ==(int2 v, int2 u) => v.X == u.X && v.Y == u.Y;

        public static bool operator !=(int2 v, int2 u) => !(v == u);

        public override bool Equals(object obj) => obj is int2 other && this == other;

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}
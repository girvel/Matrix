using System.Collections.Specialized;

namespace Matrix.Tools
{
    public struct Vector2
    {
        public static readonly Vector2
            Zero = new Vector2(0, 0),
            Up = new Vector2(0, -1),
            Right = new Vector2(1, 0),
            Down = new Vector2(0, 1),
            Left = new Vector2(-1, 0);
        
        
        public readonly int X, Y;

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(
                v1.X + v2.X,
                v1.Y + v2.Y);
        }

        public static bool operator <(Vector2 v, Vector2 size)
        {
            return v.X < size.X && v.Y < size.Y;
        }

        public static bool operator >(Vector2 size, Vector2 v) => v < size;

        public static bool operator <=(Vector2 v, Vector2 size)
        {
            return v.X <= size.X && v.Y <= size.Y;
        }

        public static bool operator >=(Vector2 size, Vector2 v) => v <= size;

        public bool Inside(Vector2 a, Vector2? b = null)
        {
            return b == null 
                ? Zero <= this && this < a 
                : a <= this && this < b;
        }

        public override string ToString() => $"{{{X}; {Y}}}";
    }
}
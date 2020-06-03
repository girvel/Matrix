namespace Matrix.Tools
{
    public struct Vector2
    {
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

        public static bool operator >(Vector2 v, Vector2 size) => !(v < size);
    }
}
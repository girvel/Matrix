namespace Matrix.Tools
{
    public static class ArrayHelper
    {
        public static T[] DeepClone<T>(this T[] array) where T : IDeeplyCloneable<T>
        {
            var result = new T[array.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = array[i].DeepClone();
            }

            return result;
        }
    }
}
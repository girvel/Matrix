namespace Matrix.Tools
{
    public interface IDeeplyCloneable<out T> where T : IDeeplyCloneable<T>
    {
        T DeepClone();
    }
}
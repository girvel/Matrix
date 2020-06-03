using System;
using System.Collections;
using System.Collections.Generic;

namespace Matrix.Tools
{
    public class Field<T> : IEnumerable<(Vector2 v, T t)>
    {
        protected readonly T[] InternalArray;

        public Vector2 Size { get; }

        public Field(Vector2 size, Func<Vector2, T> filler = null)
        {
            InternalArray = new T[size.X * size.Y];
            Size = size;

            if (filler == null) return;
            
            foreach (var (v, _) in this)
            {
                this[v] = filler(v);
            }
        }

        public T this[Vector2 v] 
        {
            get => InternalArray[v.X + v.Y * Size.X];
            set => InternalArray[v.X + v.Y * Size.X] = value;
        }

        private IEnumerable<(Vector2 v, T t)> _getEnumerable()
        {
            for (var y = 0; y < Size.Y; y++)
            {
                for (var x = 0; x < Size.X; x++)
                {
                    var v = new Vector2(x, y);
                    yield return (v, this[v]);
                }
            }
        }
        
        public IEnumerator<(Vector2 v, T t)> GetEnumerator()
        {
            return _getEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
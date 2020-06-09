using System;
using System.Collections;
using System.Collections.Generic;

namespace Matrix.Tools
{
    public class Field<T> : IEnumerable<(int2 v, T t)>
    {
        protected readonly T[] InternalArray;

        public int2 Size { get; }

        public Field(int2 size, Func<int2, T> filler = null)
        {
            InternalArray = new T[size.X * size.Y];
            Size = size;

            if (filler == null) return;
            
            foreach (var (v, _) in this)
            {
                this[v] = filler(v);
            }
        }

        public T this[int2 v] 
        {
            get => InternalArray[v.X + v.Y * Size.X];
            set => InternalArray[v.X + v.Y * Size.X] = value;
        }

        private IEnumerable<(int2 v, T t)> _getEnumerable()
        {
            for (var y = 0; y < Size.Y; y++)
            {
                for (var x = 0; x < Size.X; x++)
                {
                    var v = new int2(x, y);
                    yield return (v, this[v]);
                }
            }
        }
        
        public IEnumerator<(int2 v, T t)> GetEnumerator()
        {
            return _getEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
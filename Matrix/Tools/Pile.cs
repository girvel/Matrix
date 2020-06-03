using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Matrix.Tools
{
    public class Pile<TKey, TValue> : IEnumerable<TValue>
    {
        private List<TValue> _values = new List<TValue>();
        private List<TKey> _keys = new List<TKey>();

        public TValue this[TKey key]
        {
            get => _values[_keys.IndexOf(key)];
            set
            {
                var index = _keys.IndexOf(key);

                if (index == -1)
                {
                    _keys.Add(key);
                    _values.Add(value);
                    return;
                }

                _values[index] = value;
            }
        }

        public IEnumerable<TValue> SliceFrom(TKey from)
        {
            return _values.Skip(_keys.IndexOf(from));
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _values).GetEnumerator();
        }
    }
}
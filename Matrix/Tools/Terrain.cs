using System.Collections;
using System.Diagnostics;

namespace Matrix.Tools
{
    public class Terrain : IEnumerable
    {
        public const byte Size = 3;
        
        public const byte
            Water = 0,
            Lava = 1,
            Land = 2;
        
        private readonly byte[] _values;

        public Terrain(params byte[] values)
        {
            Debug.Assert(values.Length == Size);
            _values = values;
        }

        public byte this[byte key]
        {
            get => _values[key];
            set => _values[key] = value;
        }

        public byte SliceFrom(byte from)
        {
            byte result = 0;
            for (; from < Size; from++)
            {
                result += _values[from];
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }
}
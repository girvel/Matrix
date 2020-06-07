using System.Collections;
using System.Diagnostics;

namespace Matrix.Tools
{
    public class Terrain : IEnumerable
    {
        public const byte Size = 4;
        
        public const byte
            CLOUDS = 0,
            WATER = 1,
            LAVA = 2,
            LAND = 3;
        
        private readonly byte[] _values = new byte[Size];

        public Terrain(params byte[] values)
        {
            Debug.Assert(values.Length == Size);
            for (var i = 0; i < Size; i++)
            {
                _values[i] = values[i];
            }
        }

        public byte this[byte key]
        {
            get => _values[key];
            set => _values[key] = value;
        }

        public byte Clouds
        {
            get => _values[CLOUDS];
            set => _values[CLOUDS] = value;
        }

        public byte Water
        {
            get => _values[WATER];
            set => _values[WATER] = value;
        }

        public byte Lava
        {
            get => _values[LAVA];
            set => _values[LAVA] = value;
        }

        public byte Land
        {
            get => _values[LAND];
            set => _values[LAND] = value;
        }

        public byte SliceFrom(byte from = 0)
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
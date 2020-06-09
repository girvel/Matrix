using System;
using System.Collections.Generic;
using System.Linq;
using Matrix.Tools;

namespace Matrix.Core
{
    public abstract class System
    {
        public Session Session { get; set;  }

        protected abstract void _update();

        protected int Rarity = 1;
        private int _currentRarity = 0;

        public void Update()
        {
            using var timer = new Clocks.Timer(this);
            
            _currentRarity++;
            if (_currentRarity < Rarity) return;

            _currentRarity = 0;

            _update();
        }

        protected System()
        {
            ConstantAttribute.UpdateConstants(this);
        }

        public static Dictionary<string, double> GetFrequency(IEnumerable<System> systems)
        {
            var enumerable = systems as System[] ?? systems.ToArray();
            return enumerable
                .Select(s => new KeyValuePair<string, double>(s.GetType().Name, 1 / Clocks.ResumeData(s)))
                .Append(new KeyValuePair<string, double>("Total", 1 / enumerable.Sum(Clocks.ResumeData)))
                .ToDictionary(p => p.Key, p => p.Value);
        }
    }
}
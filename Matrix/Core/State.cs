using System;
using System.Linq;
using Angem;
using Matrix.Tools;

namespace Matrix.Core
{
    public class State
    {
        public Field<Region> Field, NextField;
        public Random Random;
        
        public DateTime Date = new DateTime(1, 1, 1);

        public static readonly int2[] Directions = {int2.Forward, int2.Right, int2.Back, int2.Left};
        
        public static readonly int2[][] RandomizedDirections = Enumerable
            .Range(0, Directions.Length)
            .Select(i => Enumerable
                .Range(i, Directions.Length)
                .Select(j => Directions[j % Directions.Length])
                .ToArray())
            .ToArray();
    }
}
using System;
using Matrix.Tools;

namespace Matrix.Core
{
    public class State
    {
        public Field<Region> Field;
        public Random Random;
        
        public DateTime Date = new DateTime(1, 1, 1);
    }
}
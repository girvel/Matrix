using System;

namespace Precalc
{
    public class NaturalFunction<TReturn>
    {
        private readonly TReturn[] PrecalculatedValues;

        private readonly Func<int, TReturn> Function;

        private readonly int StartingValue;

        public NaturalFunction(Func<int, TReturn> function, int precalcSize, int startingValue = 0)
        {
            Function = function;
            StartingValue = startingValue;

            PrecalculatedValues = new TReturn[precalcSize];
            for (var i = 0; i < PrecalculatedValues.Length; i++)
            {
                PrecalculatedValues[i] = Function(i + StartingValue);
            }
        }

        public TReturn Calculate(int argument)
        {
            return StartingValue <= argument && argument < PrecalculatedValues.Length + StartingValue
                ? PrecalculatedValues[argument - StartingValue] 
                : Function(argument);
        }
    }
}
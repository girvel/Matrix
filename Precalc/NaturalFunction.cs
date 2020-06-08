using System;

namespace Precalc
{
    public class NaturalFunction<TReturn>
    {
        private readonly TReturn[] _precalculatedValues;

        private readonly Func<int, TReturn> _function;

        private readonly int _startingValue;

        public NaturalFunction(Func<int, TReturn> function, int precalcSize, int startingValue = 0)
        {
            _function = function;
            _startingValue = startingValue;

            _precalculatedValues = new TReturn[precalcSize];
            for (var i = 0; i < _precalculatedValues.Length; i++)
            {
                _precalculatedValues[i] = _function(i + _startingValue);
            }
        }

        public TReturn Calculate(int argument)
        {
            return _startingValue <= argument && argument < _precalculatedValues.Length + _startingValue
                ? _precalculatedValues[argument - _startingValue] 
                : _function(argument);
        }
    }
}
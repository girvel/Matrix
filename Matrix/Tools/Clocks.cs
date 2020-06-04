using System;
using System.Collections.Generic;
using System.Linq;

namespace Matrix.Tools
{
    public static class Clocks
    {
        private static Dictionary<object, List<double>> _results = new Dictionary<object, List<double>>();

        public class Timer : IDisposable
        {
            private DateTime _started;
            private object _key;
            
            public Timer(object key)
            {
                if (!_results.ContainsKey(key))
                {
                    _results[key] = new List<double>();
                }
                _key = key;
                _started = DateTime.Now;
            }

            public void Dispose()
            {
                var totalSeconds = (DateTime.Now - _started).TotalSeconds;
                _results[_key].Add(totalSeconds);
            }
        }

        public static double ResumeData(object key)
        {
            return _results.TryGetValue(key, out List<double> l) && l.Count > 0
                ? l.Average()
                : -1;
        } 
    }
}
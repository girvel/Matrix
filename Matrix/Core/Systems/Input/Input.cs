using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Matrix.Core.Systems.Input
{
    public class Input : System
    {
        private readonly RegionDisplayer _displayer;
        
        private readonly Dictionary<ConsoleKey, Action> _inputActions;
        
        public Input(RegionDisplayer displayer)
        {
            _displayer = displayer;
            _inputActions = GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Select(m => (m, attr: m.GetCustomAttribute<UiActionAttribute>()))
                .Where(tuple => tuple.attr != null)
                .ToDictionary(
                    tuple => tuple.attr.Key, 
                    tuple => new Action(() => tuple.m.Invoke(this, new object[0])));
        }
        
        protected override void _update()
        {
            if (_inputActions.TryGetValue(Console.ReadKey(true).Key, out var a)) a();
        }



        [UiAction(ConsoleKey.C, "Clouds on/off")]
        private void CloudsVisibility()
        {
            _displayer.AreCloudsVisible ^= true;
        }
    }
}
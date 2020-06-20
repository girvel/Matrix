using System;

namespace Matrix.Core.Systems.Input
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UiActionAttribute : Attribute
    {
        public ConsoleKey Key { get; }
        public string Description { get; }

        public UiActionAttribute(ConsoleKey key, string description)
        {
            Key = key;
            Description = description;
        }
    }
}
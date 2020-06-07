using System;
using Matrix.Tools;

namespace Matrix.Core
{
    public abstract class System
    {
        public Session Session { get; set;  }

        public abstract void Update();

        protected System()
        {
            ConstantAttribute.UpdateConstants(this);
        }
    }
}
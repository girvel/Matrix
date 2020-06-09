using Matrix.Tools;
using NotImplementedException = System.NotImplementedException;

namespace Matrix.Core
{
    public abstract class RegionSystem : System
    {
        protected abstract void UpdateEntity(int2 position, Region region);

        public override void Update()
        {
            foreach (var (position, region) in Session.Field)
            {
                UpdateEntity(position, region);
            }
        }
    }
}
using System.Threading.Tasks;
using Matrix.Tools;

namespace Matrix.Core
{
    public abstract class RegionSystem : System
    {
        protected abstract void UpdateEntity(int2 position, Region region);

        protected override void _update()
        {
            foreach (var (position, region) in Session.Field)
            {
                UpdateEntity(position, region);
            }
        }
    }
}
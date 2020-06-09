using System.Linq;

namespace Matrix.Core.Systems
{
    public class Cleaning : System
    {
        public override void Update()
        {
            var min = Session.Field.Min(t => t.t.Terrain.Land);
            foreach (var (v, region) in Session.Field)
            {
                region.Terrain.Land -= min;
            }
        }
    }
}
using System.Linq;

namespace Matrix.Core.Systems
{
    public class Cleaning : System
    {
        public Cleaning()
        {
            Rarity = 50;
        }
        
        protected override void _update()
        {
            var min = State.Field.Min(t => t.content.Terrain.Land);
            foreach (var (v, region) in State.Field)
            {
                region.Terrain.Land -= min;
            }
        }
    }
}
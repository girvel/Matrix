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
            var min = Session.Field.Min(t => t.content.Terrain.Land);
            foreach (var (v, region) in Session.Field)
            {
                region.Terrain.Land -= min;
            }
        }
    }
}
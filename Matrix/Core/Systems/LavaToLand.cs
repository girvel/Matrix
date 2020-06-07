using System;
using Matrix.Tools;

namespace Matrix.Core.Systems
{
    public class LavaToLand : System
    {
        public readonly Vector2[] Directions =
        {
            Vector2.Zero, Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left,
        };
        
        public override void Update()
        {
            foreach (var (v, region) in Session.Field)
            {
                if (Session.Random.NextDouble() >= 0.95) continue;

                foreach (var dir in Directions)
                {
                    if (region.Terrain[Terrain.LAVA] <= 0) break;
                    if (!(v + dir).Inside(Session.Field.Size)) continue;
                    
                    var other = Session.Field[v + dir];
                    
                    if (other.Terrain[Terrain.WATER] <= 0) continue;
                    
                    region.Terrain[Terrain.LAND]++;
                    region.Terrain[Terrain.LAVA]--;
                    other.Terrain[Terrain.WATER]--;
                    other.Terrain[Terrain.CLOUDS]++;
                    break;
                }

                if (region.Terrain[Terrain.LAVA] > 0 && Session.Random.NextDouble() < 0.1)
                {
                    region.Terrain[Terrain.LAND]++;
                    region.Terrain[Terrain.LAVA]--;
                }
            }
        }
    }
}
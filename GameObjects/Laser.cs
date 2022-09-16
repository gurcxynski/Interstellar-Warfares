using Microsoft.Xna.Framework;
using Spaceshooter.Config;
using Spaceshooter.Core;

namespace Spaceshooter.GameObjects
{
    public class Laser : GameObject
    {
        public bool hostile;
        public Laser(Vector2 start, bool isHostile)
        {
            hostile = isHostile;
            Texture = Game1.self.textures["laser"];
            Position = start;
            Velocity = (hostile ? -1 : 1) * Configuration.baseLaserVel;
        }
    }
}

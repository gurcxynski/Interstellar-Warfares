using Microsoft.Xna.Framework;
using Spaceshooter.Config;
using Spaceshooter.Core;

namespace Spaceshooter.GameObjects
{
    internal class Laser : GameObject
    {
        public Laser(Vector2 start)
        {
            Texture = Game1.self.textures["laser"];
            Position = start;
            Velocity = Configuration.baseLaserVel;
        }
    }
}

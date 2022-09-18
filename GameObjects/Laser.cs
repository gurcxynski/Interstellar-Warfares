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
            HP = 1; //to prevent disappearing

            hostile = isHostile;
            Texture = Game1.self.textures[isHostile ? "laserEnemy" : "laser"]; // to determine laser color

            Position = start;
            Velocity = (hostile ? -1 : 1) * Configuration.baseLaserVel; // to determine laser direction
        }
    }
}

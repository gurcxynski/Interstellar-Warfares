using Microsoft.Xna.Framework;
using Spaceshooter.GameObjects;

namespace Spaceshooter.Core
{
    public class Cannon
    {
        readonly GameObject parent; // object that cannon is sticked to
        Vector2 relative; // relative position to parent
        readonly bool hostile;
        readonly float shootingSpeed;
        public double lastShot = 0;

        public Cannon(GameObject parentArg, Vector2 relativeArg, float speed, bool isHostile)
        {
            parent = parentArg;
            relative = relativeArg;
            shootingSpeed = speed;
            hostile = isHostile;
        }
        // shooting every (1/shooting speed) seconds, so higher shootingSpeed = faster shooting
        public void Update(GameTime UpdateTime)
        {
            if (UpdateTime.TotalGameTime.TotalSeconds - lastShot < (1/shootingSpeed)) return;
            lastShot = UpdateTime.TotalGameTime.TotalSeconds;
            Game1.self.activeScene.toAdd.Add(new Laser(parent.Position + relative, hostile));
        }
    }
}

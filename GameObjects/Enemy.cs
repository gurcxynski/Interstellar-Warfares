using Microsoft.Xna.Framework;
using Spaceshooter.Config;
using Spaceshooter.Core;

namespace Spaceshooter.GameObjects
{
    public abstract class Enemy : GameObject
    {
        public double lastShot = 0;
        public double shootingSpeed = 1;
        public Enemy(Vector2 arg)
        {
            Position = arg;
        }
        public override void Update(GameTime UpdateTime)
        {
            if (Game1.self.activeScene.paused) return;

            base.Update(UpdateTime);

            if (UpdateTime.TotalGameTime.TotalSeconds - lastShot < shootingSpeed) return;
            lastShot = UpdateTime.TotalGameTime.TotalSeconds;
            Game1.self.activeScene.toAdd.Add(new Laser(new(Position.X + Texture.Width / 2, Position.Y + Texture.Height), true));

        }
    }
}

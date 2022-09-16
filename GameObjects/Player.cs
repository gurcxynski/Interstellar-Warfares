using Microsoft.Xna.Framework;
using Spaceshooter.Config;
using Spaceshooter.Core;
using System;

namespace Spaceshooter.GameObjects
{
    public class Player : GameObject
    {
        public double lastShot = 0;
        public double shootingSpeed = 0.3;
        public Player()
        {
            Texture = Game1.self.textures["player"];
            Position = new Vector2(250, 700);
            Velocity = Vector2.Zero;
            HP = 2;
        }

        public override void Update(GameTime UpdateTime)
        {
            if (Position.X < 0) Position = new(0, Position.Y);
            else if (Position.X > Configuration.windowSize.X) Position = new(Configuration.windowSize.X, Position.Y);

            if (Position.Y < 0) Position = new(Position.X, 0);
            else if (Position.Y > Configuration.windowSize.Y) Position = new(Position.X, Configuration.windowSize.Y);

            base.Update(UpdateTime);

            if (UpdateTime.TotalGameTime.TotalSeconds - lastShot < shootingSpeed) return;
            lastShot = UpdateTime.TotalGameTime.TotalSeconds;
            Game1.self.activeScene.toAdd.Add(new Laser(new(Position.X + Texture.Width / 2, Position.Y - Texture.Height), false));
        }
        
    }
}

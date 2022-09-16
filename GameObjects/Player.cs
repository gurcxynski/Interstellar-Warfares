using Microsoft.Xna.Framework;
using Spaceshooter.Config;
using Spaceshooter.Core;
using System;

namespace Spaceshooter.GameObjects
{
    public class Player : GameObject
    {
        public double lastShot = 0;
        public double shootingSpeed;
        public Vector2 acceleration;
        public double hasBeenHit = 0;
        public Player(Level level)
        {
            Texture = Game1.self.textures["player"];
            Position = new (Configuration.windowSize.X / 2, Configuration.windowSize.Y - 100);
            Velocity = Vector2.Zero;
            HP = level.PlayerHP;
            shootingSpeed = level.PlayerShootingSpeed;
            acceleration = new();
        }

        public override void Update(GameTime UpdateTime)
        {
            if (Position.X < 0 || Position.X + Texture.Width > Configuration.windowSize.X) Velocity *= new Vector2(-1, 1);

            if (Position.Y < 0 || Position.Y + Texture.Height > Configuration.windowSize.Y) Velocity *= new Vector2(1, -1);


            base.Update(UpdateTime);

            Velocity += acceleration;
            Velocity *= Configuration.dampening;

            if (UpdateTime.TotalGameTime.TotalSeconds - lastShot < shootingSpeed) return;
            lastShot = UpdateTime.TotalGameTime.TotalSeconds;
            Game1.self.activeScene.toAdd.Add(new Laser(new(Position.X + Texture.Width / 2, Position.Y - Texture.Height), false));
        }
        
    }
}

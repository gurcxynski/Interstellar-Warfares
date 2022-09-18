using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spaceshooter.Config;
using Spaceshooter.Core;

namespace Spaceshooter.GameObjects
{
    public class Player : GameObject
    {
        public double lastShot = 0;
        public double hasBeenHit = 0;
        public double shootingSpeed;
        protected Cannon cannon;

        public Vector2 acceleration;

        public int MaxHP;
        public int lives;

        public Player(Level level)
        {
            Texture = Game1.self.textures["player"];

            Position = new((Configuration.windowSize.X - Texture.Width) / 2, Configuration.windowSize.Y - 100);
            Velocity = new();
            acceleration = new();

            MaxHP = HP = level.PlayerHP;
            shootingSpeed = level.PlayerShootingSpeed;
        }

        public override void Update(GameTime UpdateTime)
        {
            if (cannon is null) cannon = new(this, new(Texture.Width / 2, 0), shootingSpeed, false);

            // create boundary bouncing effect

            if (Position.X < 0 || Position.X + Texture.Width > Configuration.windowSize.X) Velocity *= new Vector2(-1, 1);
            if (Position.Y < 0 || Position.Y + Texture.Height > Configuration.windowSize.Y) Velocity *= new Vector2(1, -1);

            // adjust movement by dampening to create more realistic movement

            base.Update(UpdateTime);
            Velocity += acceleration;
            Velocity *= Configuration.dampening;

            // shoot lasers if off cooldown

            cannon.Update(UpdateTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new((int)Position.X, (int)(Position.Y + Texture.Height + 5), (int)(Texture.Width * ((float)HP) / MaxHP), 4);

            spriteBatch.Draw(Texture, Position, Color.White);
            spriteBatch.Draw(Game1.self.textures["red"], rectangle, Color.White);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceshooter.Core
{
    public class GameObject
    {
        Vector2 lastVel = Vector2.Zero;
        public int HP = 5;

        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public virtual void Update(GameTime UpdateTime)
        {
            float passed = (float)UpdateTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * passed;
        }
        public void UnPause()
        {
            Velocity = lastVel;
            lastVel = Vector2.Zero;
        }
        public void Pause()
        {
            lastVel = Velocity;
            Velocity = Vector2.Zero;
        }
    }
}

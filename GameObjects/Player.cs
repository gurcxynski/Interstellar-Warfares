using Microsoft.Xna.Framework;
using Spaceshooter.Core;

namespace Spaceshooter.GameObjects
{
    internal class Player : GameObject
    {
        public Player()
        {
            Texture = Game1.self.textures["player"];
            Position = new(250, 700);
            Velocity = Vector2.Zero;
        }
    }
}

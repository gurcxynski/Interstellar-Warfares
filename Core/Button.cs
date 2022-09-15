using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using space_shooter.EasyInput;

namespace Spaceshooter.Core
{
    public abstract class Button
    {
        protected Vector2 position;
        protected Texture2D texture;
        protected bool hovered = false;
        protected Button(Vector2 arg)
        {
            Game1.mouse.OnMouseButtonPressed += OnClick;
            texture = Game1.self.textures["button"];
            position = arg;
        }
        public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(texture, position, Color.White);
        protected bool EnteredButton()
        {
            if (Game1.mouse.Position.X < position.X + texture.Width &&
                    Game1.mouse.Position.X > position.X &&
                    Game1.mouse.Position.Y < position.Y + texture.Height &&
                    Game1.mouse.Position.Y > position.Y) return true;
            return false;
        }
        public virtual void Update()
        {
            hovered = EnteredButton();
        }
        protected void OnClick(MouseButtons button)
        {
            if (hovered && button == MouseButtons.Left)
            {
                Action();
            }
        }
        protected abstract void Action();
    }
}

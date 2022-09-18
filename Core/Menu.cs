using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spaceshooter.Config;
using System.Collections.Generic;

namespace Spaceshooter.Core
{
    public abstract class Menu
    {
        readonly protected List<Button> buttons;

        public Vector2 Position;

        protected bool isActive = false;

        protected Texture2D back;
        protected Texture2D title;
        public Menu() => buttons = new();
        public virtual void Initialize()
        {
            back = Game1.self.textures["menubck"];
            title = Game1.self.textures["title"];

            Position = new((Configuration.windowSize.X - back.Width) / 2, 150);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(title, new Vector2((Configuration.windowSize.X - title.Width) / 2, 20), Color.White);

            spriteBatch.Draw(back, Position, Color.White);
            buttons.ForEach(delegate (Button btn) { btn.Draw(spriteBatch); });
        }
        public void Activate()
        {
            if (isActive) return;
            buttons.ForEach(delegate (Button btn) { btn.Activate(); });
            isActive = true;
        }
        public void Deactivate()
        {
            if (!isActive) return;
            buttons.ForEach(delegate (Button btn) { btn.Deactivate(); });
            isActive = false;
        }
    }
}

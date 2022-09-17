using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Spaceshooter.Config;
using Spaceshooter.Buttons;
using Microsoft.Xna.Framework;

namespace Spaceshooter.Core
{
    public abstract class Menu
    {
        readonly protected List<Button> buttons;
        bool active = false;
        public Menu()
        {
            buttons = new();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D back = Game1.self.textures["menubck"];
            Texture2D title = Game1.self.textures["title"];
            spriteBatch.Draw(back, new Vector2((Configuration.windowSize.X - back.Width) / 2, 150), Color.White);
            spriteBatch.Draw(title, new Vector2((Configuration.windowSize.X - title.Width) / 2, 20), Color.White);
            buttons.ForEach(delegate (Button btn) { btn.Draw(spriteBatch); });
        }
        public void Update()
        {
            buttons.ForEach(delegate (Button btn) { btn.Update(); });
        }
        public void Activate()
        {
            if (active) return;
            buttons.ForEach(delegate (Button btn) { btn.Activate(); });
            active = true;
        }
        public void Deactivate()
        {
            if (!active) return;
            buttons.ForEach(delegate (Button btn) { btn.Deactivate(); });
            active = false;
        }
        public abstract void Initialize();
    }
}

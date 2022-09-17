using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Spaceshooter.Config;
using Spaceshooter.Buttons;
using Microsoft.Xna.Framework;

namespace Spaceshooter.Core
{
    public class Menu
    {
        readonly protected List<Button> buttons;
        public Menu()
        {
            buttons = new();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            buttons.ForEach(delegate (Button btn) { btn.Draw(spriteBatch); });
        }
        public void Update()
        {
            buttons.ForEach(delegate (Button btn) { btn.Update(); });
        }
        public void Initialize()
        {
            buttons.Add(new TestButton(new Vector2(100, 100)));
        }
    }
}

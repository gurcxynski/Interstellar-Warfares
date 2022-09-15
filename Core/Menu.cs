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
            if (!GameState.menuEnabled) return;
            buttons.ForEach(delegate (Button btn) { btn.Draw(spriteBatch); });
        }
        public void Update()
        {
            if (!GameState.menuEnabled) return;
            buttons.ForEach(delegate (Button btn) { btn.Update(); });
        }
        public void Enable()
        {
            if (GameState.menuEnabled) throw new Exception("Already enabled menu!");
            GameState.menuEnabled = true;
        }
        public void Disable()
        {
            if (!GameState.menuEnabled) throw new Exception("Already disabled menu!");
            GameState.menuEnabled = false;
        }

        public void Initialize()
        {
            buttons.Add(new testButton(new Vector2(100, 100)));
        }
    }
}

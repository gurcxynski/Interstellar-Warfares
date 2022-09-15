using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using space_shooter.EasyInput;
using Spaceshooter.Config;
using Spaceshooter.Core;
using System.Collections.Generic;

namespace Spaceshooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Game1 self;

        public static EasyKeyboard keyboard;
        public static EasyMouse mouse;

        public Scene activeScene;
        Menu _menu;

        public Dictionary<string, Texture2D> textures = new();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            self = this;
            _graphics.PreferredBackBufferWidth = (int)Configuration.windowSize.X;
            _graphics.PreferredBackBufferHeight = (int)Configuration.windowSize.Y;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            keyboard = new();
            mouse = new();

            activeScene = new();
            _menu = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textures["player"] = Content.Load<Texture2D>("space ship");
            textures["laser"] = Content.Load<Texture2D>("laser");
            textures["button"] = Content.Load<Texture2D>("buttons");
            textures["enemy1"] = Content.Load<Texture2D>("smallenemy");

            _menu.Initialize();
            activeScene.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            mouse.Update();
            keyboard.Update();

            if(GameState.menuEnabled) _menu.Update();
            else activeScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            if (GameState.menuEnabled) _menu.Draw(_spriteBatch);
            else activeScene.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void EnableMenu()
        {
            _menu.Enable();
            GameState.menuEnabled = true;
        }

        public void DisableMenu()
        {
            _menu.Disable();
            GameState.menuEnabled = false;
        }
    }
}
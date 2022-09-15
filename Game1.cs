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

        public static EasyKeyboard _keyboard;
        private Scene _scene;

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
            _keyboard = new();
            _scene = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textures["player"] = Content.Load<Texture2D>("space ship");
            textures["laser"] = Content.Load<Texture2D>("laser");

            _scene.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            _scene.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _scene.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
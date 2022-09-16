using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.EasyInput;
using Spaceshooter.Config;
using Spaceshooter.Core;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Spaceshooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Game1 self;

        public static EasyKeyboard keyboard;
        public static EasyMouse mouse;


        public State state;

        public Levels levels;
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
            state = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textures["back"] = Content.Load<Texture2D>("back");
            textures["player"] = Content.Load<Texture2D>("space ship");
            textures["laser"] = Content.Load<Texture2D>("laser2");
            textures["laserEnemy"] = Content.Load<Texture2D>("laser");
            textures["button"] = Content.Load<Texture2D>("buttons");
            textures["enemy1"] = Content.Load<Texture2D>("smallenemy");
            textures["enemy2"] = Content.Load<Texture2D>("enemy2");
            textures["boss"] = Content.Load<Texture2D>("Turtle");
            

            string path = "levels.json";
            string jsonString = File.ReadAllText(path);
            levels = JsonSerializer.Deserialize<Levels>(jsonString);

            _menu.Initialize();
            activeScene.Initialize(levels.Get(0));
        }

        protected override void Update(GameTime gameTime)
        {
            mouse.Update();
            keyboard.Update();

            if (state.state == State.GameState.Menu || state.state == State.GameState.Paused) _menu.Update();
            else activeScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(textures["back"], Vector2.Zero, Color.White);

            if (state.state == State.GameState.Menu || state.state == State.GameState.Paused) _menu.Draw(_spriteBatch);
            else activeScene.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
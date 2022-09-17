using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Monogame.EasyInput;
using SharpDX.Direct3D9;
using Spaceshooter.Buttons;
using Spaceshooter.Config;
using Spaceshooter.Core;
using Spaceshooter.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace Spaceshooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        public static Game1 self;

        public static EasyKeyboard keyboard;
        public static EasyMouse mouse;


        public SoundEffect music;

        public State state;
        public SoundEffectInstance instance;
        public Levels levels;
        public Scene activeScene;
        public StartScreen starting;
        public LevelSelect levelSelect;
        public PauseMenu menu;


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
            Window.Title = "Interstellar Warfares";
        }

        protected override void Initialize()
        {
            keyboard = new();
            mouse = new();

            activeScene = new();
            menu = new();
            starting = new();
            state = new();
            levelSelect = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textures["back"] = Content.Load<Texture2D>("BG");
            textures["gameWon"] = Content.Load<Texture2D>("Header");
            textures["title"] = Content.Load<Texture2D>("title");
            textures["red"] = Content.Load<Texture2D>("red");
            textures["player"] = Content.Load<Texture2D>("ship");
            textures["laser"] = Content.Load<Texture2D>("laser2");
            textures["laserEnemy"] = Content.Load<Texture2D>("laser");
            textures["menubck"] = Content.Load<Texture2D>("Window");
            textures["button"] = Content.Load<Texture2D>("buttonclean");
            textures["exitbutton"] = Content.Load<Texture2D>("Exit_BTN");
            textures["playbutton"] = Content.Load<Texture2D>("Start_BTN");
            textures["menubutton"] = Content.Load<Texture2D>("menubutton");
            textures["resumebutton"] = Content.Load<Texture2D>("resume");
            textures["turnonmusic"] = Content.Load<Texture2D>("Music_BTN");
            textures["turnoffmusic"] = Content.Load<Texture2D>("Music2_BTN");
            textures["enemy1"] = Content.Load<Texture2D>("smallenemy");
            textures["enemy2"] = Content.Load<Texture2D>("enemy2");
            textures["boss"] = Content.Load<Texture2D>("Turtle");
            textures["bossnew"] = Content.Load<Texture2D>("Ship_Icon");
            textures["level0"] = Content.Load<Texture2D>("level1");
            textures["level1"] = Content.Load<Texture2D>("level2");
            textures["level2"] = Content.Load<Texture2D>("level3");
            textures["level3"] = Content.Load<Texture2D>("level4");
            textures["level4"] = Content.Load<Texture2D>("level5");
            textures["levelselect"] = Content.Load<Texture2D>("levelselect");

            music = Content.Load<SoundEffect>("spaceship");

            instance = music.CreateInstance();
            instance.IsLooped = true;
            instance.Play();
            instance.Volume = 0.1f;

            string path = "levels.json";
            string jsonString = File.ReadAllText(path);
            levels = JsonSerializer.Deserialize<Levels>(jsonString);

            menu.Initialize();
            starting.Initialize();
            levelSelect.Initialize();

            starting.Activate();

            activeScene.Initialize(levels.Get(0));
        }

        protected override void Update(GameTime gameTime)
        {
            mouse.Update();
            keyboard.Update();

            if (state.state == State.GameState.Paused) menu.Update();
            else if (state.state == State.GameState.StartMenu) starting.Update();
            else activeScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();


            int milliseconds = (int)(gameTime.TotalGameTime.TotalSeconds * 1000);
            int milliModulo = milliseconds % 10000;
            float offsetPercent = milliModulo / 10000f;
            int offset = (int)(offsetPercent * Configuration.windowSize.Y);
            var rectA = new Rectangle(0, offset, (int)Configuration.windowSize.X, (int)Configuration.windowSize.Y);
            var rectB = new Rectangle(0, offset - (int)Configuration.windowSize.Y, (int)Configuration.windowSize.X, (int)Configuration.windowSize.Y);
            spriteBatch.Draw(textures["back"], rectB, Color.White);
            spriteBatch.Draw(textures["back"], rectA, Color.White);


            if (state.state == State.GameState.Paused) menu.Draw(spriteBatch);
            else if (state.state == State.GameState.StartMenu) starting.Draw(spriteBatch);
            else if (state.state == State.GameState.LevelSelect) levelSelect.Draw(spriteBatch);
            else activeScene.Draw(spriteBatch);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
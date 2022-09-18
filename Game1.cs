using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Monogame.EasyInput;
using Spaceshooter.Config;
using Spaceshooter.Core;
using Spaceshooter.Menus;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Spaceshooter
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        public static Game1 self;

        // Mouse and keyboard from EasyInput

        public static EasyKeyboard keyboard;
        public static EasyMouse mouse;

        //Music data

        SoundEffect music;
        public SoundEffectInstance instance;

        // Game State

        public State state;

        // All level data

        public Levels levels;

        // Game Scene

        public Scene activeScene;

        // All menus

        public StartScreen starting;
        public LevelSelect levelSelect;
        public PauseMenu menu;

        // Texture list

        public Dictionary<string, Texture2D> textures = new();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            self = this;

            Window.Title = "Interstellar Warfares";
            _graphics.PreferredBackBufferWidth = (int)Configuration.windowSize.X;
            _graphics.PreferredBackBufferHeight = (int)Configuration.windowSize.Y;

            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            keyboard = new();
            mouse = new();

            activeScene = new();

            menu = new();
            starting = new();
            levelSelect = new();

            state = new() { state = State.GameState.StartMenu };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loading all needed files

            textures["back"] = Content.Load<Texture2D>("BG");
            textures["title"] = Content.Load<Texture2D>("title");
            textures["menubck"] = Content.Load<Texture2D>("Window");

            textures["player"] = Content.Load<Texture2D>("ship");
            textures["enemy1"] = Content.Load<Texture2D>("smallenemy");
            textures["enemy2"] = Content.Load<Texture2D>("enemy2");
            textures["bossnew"] = Content.Load<Texture2D>("Ship_Icon");

            textures["red"] = Content.Load<Texture2D>("red");
            textures["laser"] = Content.Load<Texture2D>("laser2");
            textures["laserEnemy"] = Content.Load<Texture2D>("laser");

            textures["button"] = Content.Load<Texture2D>("buttonclean");
            textures["exitbutton"] = Content.Load<Texture2D>("Exit_BTN");
            textures["playbutton"] = Content.Load<Texture2D>("Start_BTN");
            textures["menubutton"] = Content.Load<Texture2D>("menubutton");
            textures["resumebutton"] = Content.Load<Texture2D>("resume");
            textures["turnonmusic"] = Content.Load<Texture2D>("Music_BTN");
            textures["turnoffmusic"] = Content.Load<Texture2D>("Music2_BTN");
            textures["levelselect"] = Content.Load<Texture2D>("levelselect");
            textures["pausebutton"] = Content.Load<Texture2D>("Pause_BTN");

            textures["level0"] = Content.Load<Texture2D>("level1");
            textures["level1"] = Content.Load<Texture2D>("level2");
            textures["level2"] = Content.Load<Texture2D>("level3");
            textures["level3"] = Content.Load<Texture2D>("level4");
            textures["level4"] = Content.Load<Texture2D>("level5");

            textures["select0"] = Content.Load<Texture2D>("select1");
            textures["select1"] = Content.Load<Texture2D>("select2");
            textures["select2"] = Content.Load<Texture2D>("select3");
            textures["select3"] = Content.Load<Texture2D>("select4");
            textures["select4"] = Content.Load<Texture2D>("select5");

            textures["gameWon"] = Content.Load<Texture2D>("Header");

            music = Content.Load<SoundEffect>("spaceship");

            // Playing music

            instance = music.CreateInstance();
            instance.IsLooped = true;
            instance.Play();
            instance.Volume = 0.1f;

            // Loading level data

            string path = "levels.json";
            string jsonString = File.ReadAllText(path);
            levels = JsonSerializer.Deserialize<Levels>(jsonString);

            // Initializing menus and game scene, loading level 1

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

            activeScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // Drawing moving background

            int milliseconds = (int)(gameTime.TotalGameTime.TotalSeconds * 1000);
            int milliModulo = milliseconds % 10000;
            float offsetPercent = milliModulo / 10000f;
            int offset = (int)(offsetPercent * Configuration.windowSize.Y);
            Rectangle rectA = new Rectangle(0, offset, (int)Configuration.windowSize.X, (int)Configuration.windowSize.Y);
            Rectangle rectB = new Rectangle(0, offset - (int)Configuration.windowSize.Y, (int)Configuration.windowSize.X, (int)Configuration.windowSize.Y);
            spriteBatch.Draw(textures["back"], rectB, Color.White);
            spriteBatch.Draw(textures["back"], rectA, Color.White);

            // Drawing menu/game objects

            if (state.state == State.GameState.Paused) menu.Draw(spriteBatch);
            else if (state.state == State.GameState.StartMenu) starting.Draw(spriteBatch);
            else if (state.state == State.GameState.LevelSelect) levelSelect.Draw(spriteBatch);
            else activeScene.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceshooter.Buttons;
using Spaceshooter.Config;
using Spaceshooter.EnemyTypes;
using Spaceshooter.GameObjects;
using System.Collections.Generic;
using System.Timers;

namespace Spaceshooter.Core
{
    public class Scene
    {
        public List<GameObject> objects = new();
        public List<GameObject> toAdd;
        public Player player;

        public Level level;

        public PauseButton SmallPauseButton;

        Timer Timer;

        // used when drawing communicates

        public bool drawScreen = false;
        Texture2D ScreenToDraw;

        public Scene()
        {
            Game1.keyboard.OnKeyPressed += KeyPressed;
            Game1.keyboard.OnKeyReleased += KeyReleased;
        }
        public void Initialize(Level levelArg)
        {
            level = levelArg;

            player = new(level) { lives = level.PlayerLives };
            objects.Add(player);

            SmallPauseButton = new(new(Configuration.windowSize.X - 55, 10));
            SmallPauseButton.Activate();

            // creating enemies as defined in level data

            for (int i = 0; i < level.SimpleEnemies; i++)   { objects.Add(new EasyEnemy(level)); };
            for (int i = 0; i < level.MediumEnemies; i++)   { objects.Add(new MediumEnemy(level)); };
            if (level.Boss)                                 { objects.Add(new Boss(level)); }

            ShowScreen(1000, Game1.self.textures["level" + Game1.self.state.level]);
        }
        public void Update(GameTime UpdateTime)
        {
            if (drawScreen) return;

            SmallPauseButton.Update();

            // updating every object and creating new lasers

            toAdd = new();
            objects.ForEach(delegate (GameObject item) { item.Update(UpdateTime); });
            objects.AddRange(toAdd);

            //TODO  fix occasional falling out

            // Removing lasers that hit something or have fallen out and dead enemies

            objects.RemoveAll(item => HitSomething(item, UpdateTime) || item.Position.Y < 0 - item.Texture.Height - 50 || item.Position.Y > Configuration.windowSize.Y + 50 || item.HP <= 0);

            Game1.self.state.UpdateStatus();
        }

        bool HitSomething(GameObject item, GameTime UpdateTime)
        {
            double now = UpdateTime.TotalGameTime.TotalMilliseconds;

            switch (item)
            {
                case Laser laser:
                    if (laser.hostile)
                    {
                        // if hostile laser hit player, subtract HP or remove 1 life
                        if (laser.Position.Y + laser.Texture.Height > player.Position.Y && laser.Position.Y < player.Position.Y + player.Texture.Height
                            && laser.Position.X + laser.Texture.Width > player.Position.X && laser.Position.X < player.Position.X + player.Texture.Width)
                           {
                            if (now - player.hasBeenHit < 500) return true;
                            player.HP--;
                            if (player.HP <= 0)
                            {
                                player.lives--;
                                player.HP = level.PlayerHP;
                                player.hasBeenHit = now;
                                player.Position = new((Configuration.windowSize.X - player.Texture.Width) / 2, Configuration.windowSize.Y - 100);
                            }
                            return true;
                        }
                        return false;
                    }
                    foreach (var obj in objects)
                    {
                        // if player laser hit enemy, subtract HP
                        if(!laser.hostile && obj.GetType().IsSubclassOf(typeof(Enemy)))
                        {
                            if(laser.Position.Y <= obj.Position.Y + obj.Texture.Height && laser.Position.Y < player.Position.Y + player.Texture.Height
                                && laser.Position.X + laser.Texture.Width > obj.Position.X && laser.Position.X < obj.Position.X + obj.Texture.Width)
                            {
                                obj.HP--;
                                return true;
                            }
                        }
                    }
                    return false;
                default:
                    return false;
            }
        }

        // handling player input

        void KeyPressed(Keys button)
        {
            if (Game1.self.state.state != State.GameState.Running) return;


            switch (button)
            {
                case Keys.Escape:
                    Game1.self.state.Pause();
                    break;
                case Keys.Left:
                    player.acceleration = new Vector2(-Configuration.basePlayerVel, player.acceleration.Y);
                    break;
                case Keys.Up:
                    player.acceleration = new Vector2(player.acceleration.X, -Configuration.basePlayerVel);
                    break;
                case Keys.Down:
                    player.acceleration = new Vector2(player.acceleration.X, Configuration.basePlayerVel);
                    break;
                case Keys.Right:
                    player.acceleration = new Vector2(Configuration.basePlayerVel, player.acceleration.Y);
                    break;
                default:
                    break;
            }
        }
        void KeyReleased(Keys button)
        {
            if (Game1.self.state.state != State.GameState.Running) return;


            switch (button)
            {
                case Keys.Left:
                    player.acceleration = new Vector2(0, player.acceleration.Y);
                    break;
                case Keys.Up:
                    player.acceleration = new Vector2(player.acceleration.X, 0);
                    break;
                case Keys.Down:
                    player.acceleration = new Vector2(player.acceleration.X, 0);
                    break;
                case Keys.Right:
                    player.acceleration = new Vector2(0, player.acceleration.Y);
                    break;
                default:
                    break;
            }
        }

        // drawing everything

        public void Draw(SpriteBatch spriteBatch)
        {
            objects.ForEach(delegate (GameObject item) { item.Draw(spriteBatch); });

            SmallPauseButton.Draw(spriteBatch);

            if (drawScreen) spriteBatch.Draw(ScreenToDraw, new Vector2((Configuration.windowSize.X - ScreenToDraw.Width)/2, (Configuration.windowSize.Y - ScreenToDraw.Height) / 2), Color.White);
        }

        // show texture for given amount of milliseconds

        void ShowScreen(int time, Texture2D texture)
        {

            drawScreen = true;
            ScreenToDraw = texture;
            Timer = new(time) { Enabled = true };
            Timer.Elapsed += HideScreen;
        }
        void HideScreen(object source, ElapsedEventArgs e)
        {
            Timer.Elapsed -= HideScreen;
            drawScreen = false;
        }
    }
}

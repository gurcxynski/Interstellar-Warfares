using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceshooter.Config;
using Spaceshooter.EnemyTypes;
using Spaceshooter.GameObjects;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Spaceshooter.Core
{
    public class Scene
    {
        public List<GameObject> objects = new();
        public Player player;

        public Level level;

        Timer Timer;

        public bool drawScreen = false;
        Texture2D screen;

        public Vector2 lastVel = Vector2.Zero;

        public int lives;

        public List<GameObject> toAdd;
        public Scene()
        {
            Game1.keyboard.OnKeyPressed += KeyPressed;
            Game1.keyboard.OnKeyReleased += KeyReleased;
        }
        public void Initialize(Level levelArg)
        {
            level = levelArg;
            player = new(level);

            Random rnd = new();
            List<Vector2> bossPath = new()
            {
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y))};
        ;
            for (int i = 0; i < level.SimpleEnemies; i++)
            {
                objects.Add(new EasyEnemy(level));
            };
            for (int i = 0; i < level.MediumEnemies; i++)
            {
                objects.Add(new MediumEnemy(level));
            };
            if (level.Boss)
            {
                objects.Add(new Boss(level, bossPath));
            }
            lives = level.PlayerLives;
            objects.Add(player);
            ShowScreen(2000, Game1.self.textures["levelUp"]);
        }
        public void Update(GameTime UpdateTime)
        {
            //TODO fix reload in menu timer reset
            if (drawScreen || Game1.self.state.state == State.GameState.GameWon) return;

            toAdd = new();

            objects.ForEach(delegate (GameObject item) { item.Update(UpdateTime); });

            objects.AddRange(toAdd);
            //TODO  fix falling out
            objects.RemoveAll(item => HitSomething(item, UpdateTime) || item.Position.Y < 0 - item.Texture.Height - 50 || item.Position.Y > Configuration.windowSize.Y + 50 || item.HP <= 0);

            Game1.self.state.CheckStatus();
            if (Game1.self.state.state == State.GameState.GameWon) ShowScreen(2000, Game1.self.textures["gameWon"]);
        }

        bool HitSomething(GameObject item, GameTime UpdateTime)
        {
            double now = UpdateTime.TotalGameTime.TotalMilliseconds;
            switch (item)
            {
                case Laser laser:
                    if (laser.hostile)
                    {
                        if (laser.Position.Y + laser.Texture.Height > player.Position.Y && laser.Position.Y < player.Position.Y + player.Texture.Height
                            && laser.Position.X + laser.Texture.Width > player.Position.X && laser.Position.X < player.Position.X + player.Texture.Width)
                        {
                            if (now - player.hasBeenHit < 200) return true;
                            player.HP--;
                            if (player.HP <= 0)
                            {
                                lives--;
                                player.HP = level.PlayerHP;
                                player.hasBeenHit = now;
                                player.Position = new(Configuration.windowSize.X / 2, Configuration.windowSize.Y - 100);
                            }
                            return true;
                        }
                        return false;
                    }
                    foreach (var obj in objects)
                    {
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
        void KeyPressed(Keys button)
        {
            if (Game1.self.state.state == State.GameState.Menu) return;
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
            if (Game1.self.state.state == State.GameState.Menu) return;
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
        public void Draw(SpriteBatch spriteBatch)
        {
            objects.ForEach(delegate (GameObject item) { item.Draw(spriteBatch); });
            Texture2D lvlUp = Game1.self.textures["levelUp"];
            if (drawScreen) spriteBatch.Draw(screen, new Vector2((Configuration.windowSize.X - lvlUp.Width)/2, (Configuration.windowSize.Y - lvlUp.Height) / 2), Color.White);
        }
        void StopScreen(object source, ElapsedEventArgs e)
        {
            Timer.Elapsed -= StopScreen;
            drawScreen = false;
            if (Game1.self.state.state == State.GameState.GameWon) Game1.self.state.state = State.GameState.Menu;
        }
        void ShowScreen(int time, Texture2D texture)
        {
            drawScreen = true;
            screen = texture;
            Timer = new(time) { Enabled = true };
            Timer.Elapsed += StopScreen;
        }
    }
}

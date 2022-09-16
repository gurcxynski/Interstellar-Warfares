using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceshooter.Config;
using Spaceshooter.EnemyTypes;
using Spaceshooter.GameObjects;
using System;
using System.Collections.Generic;
using System.Windows.Forms.Automation;

namespace Spaceshooter.Core
{
    public class Scene
    {
        public List<GameObject> objects = new();
        public Player player;

        public Level level;

        public Vector2 lastVel = Vector2.Zero;

        public int lives;

        public List<GameObject> toAdd;
        public List<GameObject> toRemove;
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
            List<Vector2> path = new() {
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
            };
            for (int i = 0; i < level.SimpleEnemies; i++)
            {
                objects.Add(new EasyEnemy(new Vector2(100 * i, 50), path));
            }
            lives = level.playerLives;
            objects.Add(player);
        }
        public void Update(GameTime UpdateTime)
        {
            //TODO fix reload in menu timer reset

            toAdd = new();
            toRemove = new();

            objects.ForEach(delegate (GameObject item) { item.Update(UpdateTime); });

            foreach (var item in objects)
            {
                if (HitSomething(item, UpdateTime)) toRemove.Add(item);
            }

            objects.AddRange(toAdd);

            foreach (var item in toRemove)
            {
                objects.Remove(item);
            }
            toRemove.Clear();

            objects.RemoveAll(item => item.Position.Y < 0 - item.Texture.Height - 50 || item.Position.Y > Configuration.windowSize.Y + 50 || item.HP <= 0);

            Game1.self.state.CheckStatus();
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
                            player.hasBeenHit = now;
                            if (player.HP <= 0)
                            {
                                lives--;
                                player.HP = level.PlayerHP;
                            }
                            System.Diagnostics.Debug.WriteLine($"lives: {lives} hp: {player.HP}");
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
                    player.acceleration += new Vector2(-Configuration.basePlayerVel, 0);
                    break;
                case Keys.Up:
                    player.acceleration += new Vector2(0, -Configuration.basePlayerVel);
                    break;
                case Keys.Down:
                    player.acceleration += new Vector2(0, Configuration.basePlayerVel);
                    break;
                case Keys.Right:
                    player.acceleration += new Vector2(Configuration.basePlayerVel, 0);
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
                    player.acceleration -= new Vector2(-Configuration.basePlayerVel, 0);
                    break;
                case Keys.Up:
                    player.acceleration -= new Vector2(0, -Configuration.basePlayerVel);
                    break;
                case Keys.Down:
                    player.acceleration -= new Vector2(0, Configuration.basePlayerVel);
                    break;
                case Keys.Right:
                    player.acceleration -= new Vector2(Configuration.basePlayerVel, 0);
                    break;
                default:
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            objects.ForEach(delegate (GameObject item) { item.Draw(spriteBatch); });
        }
        
    }
}

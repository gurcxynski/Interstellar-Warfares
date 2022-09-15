using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceshooter.Config;
using Spaceshooter.EnemyTypes;
using Spaceshooter.GameObjects;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Spaceshooter.Core
{
    public class Scene
    {
        public List<GameObject> objects = new();
        public Player player;

        public Vector2 lastVel = Vector2.Zero;
        public bool paused = false;


        public Scene()
        {
            Game1.keyboard.OnKeyPressed += KeyPressed;
            Game1.keyboard.OnKeyReleased += KeyReleased;
        }
        public void Initialize()
        {
            player = new();

            objects.Add(new EasyEnemy(new Vector2(100, 1)));
            objects.Add(new EasyEnemy(new Vector2(200, 50)));
            objects.Add(new EasyEnemy(new Vector2(300, 70)));
            objects.Add(player);
        }
        public void Update(GameTime UpdateTime)
        {
            List<Laser> added = new();

            // TODO: CHANGE THIS AWFUL CODE
            try { objects.ForEach(delegate (GameObject item) { item.Update(UpdateTime); }); }
            catch {  }

            CheckCollision();


            objects.RemoveAll(item => item.Position.Y < -item.Texture.Height || item.Position.Y > Configuration.windowSize.Y || item.HP <= 0);
        }
        bool HitEnemy(GameObject laser)
        {
            if (laser.GetType() != typeof(Laser) || laser.Velocity.Y > 0) return false;
            foreach (var enemy in objects)
            {
                if (enemy.GetType().IsSubclassOf(typeof(Enemy)))
                {
                    if (laser.Position.X + laser.Texture.Width >= enemy.Position.X  && laser.Position.X <= enemy.Position.X + enemy.Texture.Width
                        && laser.Position.Y <= enemy.Position.Y + enemy.Texture.Height)
                    {
                        enemy.HP -= 1;
                        return true;
                    }
                }
            }
            return false;
        }
        void CheckCollision()
        {
            objects.RemoveAll(item => HitEnemy(item));
        }
        void Pause()
        {
            objects.ForEach(delegate (GameObject item) { if (paused) item.UnPause(); else item.Pause(); });
            paused = !paused;
        }
        void KeyPressed(Keys button)
        {
            if (GameState.menuEnabled) return;
            switch (button)
            {
                case Keys.Escape:
                    Game1.self.EnableMenu();
                    break;
                case Keys.Left:
                    if (paused) break;
                    player.Velocity += new Vector2(-Configuration.basePlayerVel, 0);
                    break;
                case Keys.Up:
                    if (paused) break;
                    player.Velocity += new Vector2(0, -Configuration.basePlayerVel);
                    break;
                case Keys.Down:
                    if (paused) break;
                    player.Velocity += new Vector2(0, Configuration.basePlayerVel);
                    break;
                case Keys.Right:
                    if (paused) break;
                    player.Velocity += new Vector2(Configuration.basePlayerVel, 0);
                    break;
                default:
                    break;
            }
        }

        void KeyReleased(Keys button)
        {
            if (GameState.menuEnabled) return;
            switch (button)
            {
                case Keys.Space:
                    Pause();
                    break;
                case Keys.Left:
                    if (paused) break;
                    player.Velocity -= new Vector2(-Configuration.basePlayerVel, 0);
                    break;
                case Keys.Up:
                    if (paused) break;
                    player.Velocity -= new Vector2(0, -Configuration.basePlayerVel);
                    break;
                case Keys.Down:
                    if (paused) break;
                    player.Velocity -= new Vector2(0, Configuration.basePlayerVel);
                    break;
                case Keys.Right:
                    if (paused) break;
                    player.Velocity -= new Vector2(Configuration.basePlayerVel, 0);
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

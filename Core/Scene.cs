using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceshooter.Config;
using Spaceshooter.EnemyTypes;
using Spaceshooter.GameObjects;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Spaceshooter.Core
{
    public class Scene
    {
        public List<GameObject> objects = new();
        public Player player;

        public Level level;

        public Vector2 lastVel = Vector2.Zero;
        public bool paused = false;

        public List<GameObject> toAdd;
        public Scene()
        {
            Game1.keyboard.OnKeyPressed += KeyPressed;
            Game1.keyboard.OnKeyReleased += KeyReleased;
        }
        public void Initialize()
        {


            string fileName = "C:\\Users\\wojci\\source\\repos\\space shooter\\levels.json";
            string jsonString = File.ReadAllText(fileName);
            level = JsonSerializer.Deserialize<Level>(jsonString);

            player = new(level);

            for (int i = 0; i < level.SimpleEnemies; i++)
            {
                objects.Add(new EasyEnemy(new Vector2(100 * i, 50)));
            }

            objects.Add(player);
        }
        public void Update(GameTime UpdateTime)
        {
            toAdd = new();
            objects.ForEach(delegate (GameObject item) { item.Update(UpdateTime); });
            objects.AddRange(toAdd);
            objects.RemoveAll(item => HitEnemy(item) || HitPlayer(item) || item.Position.Y < -item.Texture.Height || item.Position.Y > Configuration.windowSize.Y || item.HP <= 0);
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
        bool HitPlayer(GameObject laser)
        {
            if (laser.GetType() != typeof(Laser) || laser.Velocity.Y < 0) return false;
                if (laser.Position.X + laser.Texture.Width >= player.Position.X  && laser.Position.X <= player.Position.X + player.Texture.Width
                    && laser.Position.Y + laser.Texture.Height >= player.Position.Y)
                {
                    player.HP -= 1;
                    return true;
                }
            return false;
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

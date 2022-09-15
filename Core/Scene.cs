using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceshooter.Config;
using Spaceshooter.GameObjects;
using System.Collections.Generic;

namespace Spaceshooter.Core
{
    internal class Scene
    {
        List<GameObject> objects = new();
        Player player;
        GameState state;
        public Scene()
        {
            Game1._keyboard.OnKeyPressed += HandleInput;
        }
        public void Initialize()
        {
            state = new();
            player = new();
            objects.Add(player);
        }
        public void Update(float UpdateTime)
        {
            Game1._keyboard.Update();
            objects.ForEach(delegate (GameObject item) { item.Update(UpdateTime); });
            objects.RemoveAll(item => item.Position.Y < -item.Texture.Height);
        }
        void Pause()
        {
            (player.Velocity, state.lastVel) = (state.lastVel, player.Velocity);
            state.paused = !state.paused;
        }
        void HandleInput(Keys button)
        {
            switch (button)
            {
                case Keys.Escape:
                    Game1.self.Exit();
                    break;
                case Keys.Space:
                    Pause();
                    break;
                case Keys.Left:
                    player.Velocity = -Configuration.basePlayerVel;
                    break;
                case Keys.Up:
                    objects.Add(new Laser(new(player.Position.X + player.Texture.Width/2, player.Position.Y - Game1.self.textures["laser"].Height)));
                    break;
                case Keys.Right:
                    player.Velocity = Configuration.basePlayerVel;
                    break;
                case Keys.Down:
                    break;
                default:
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            objects.ForEach(delegate (GameObject item) { item.Draw(spriteBatch); });
            spriteBatch.End();
        }
        
    }
}

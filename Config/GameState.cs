using Microsoft.Xna.Framework;
using Spaceshooter.GameObjects;

namespace Spaceshooter.Config
{
    public class State
    {
        public int level = 0;
        public enum GameState
        {
            Menu,
            Running,
            GameWon,
            Paused
        }
        public GameState state = GameState.Running;
        public void CheckStatus()
        {
            if (Game1.self.activeScene.lives < 0)
            {
                state = GameState.Menu;
                ToMenu();
            }
            else if(!Game1.self.activeScene.objects.Exists(item => item.GetType().IsSubclassOf(typeof(Enemy))))
            {
                level++;
                if (level >= Game1.self.levels.levels.Count) state = GameState.GameWon;
                else
                {
                    Game1.self.activeScene = new();
                    Game1.self.activeScene.Initialize(Game1.self.levels.Get(level));
                }

            }
        }
        public bool Pause()
        {
            if (state != GameState.Running) return false;
            state = GameState.Paused;
            return true;
        }
        public bool ToMenu()
        {
            state = GameState.Menu;
            return true;
        }
        public bool Play()
        {
            if(state == GameState.Paused)
            {
                state = GameState.Running;
            }
            if(state == GameState.Menu)
            {
                Game1.self.activeScene = new();
                Game1.self.activeScene.Initialize(Game1.self.levels.Get(level));
                state = GameState.Running;
            }
            return true;
        }
    }
}

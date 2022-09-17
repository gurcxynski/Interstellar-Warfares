using Microsoft.Xna.Framework;
using Spaceshooter.GameObjects;

namespace Spaceshooter.Config
{
    public class State
    {
        public int level = 0;
        public enum GameState
        {
            Running,
            GameWon,
            Paused,
            StartMenu,
            LevelSelect
        }
        public GameState state = GameState.StartMenu;
        public void CheckStatus()
        {
            if (Game1.self.activeScene.lives < 0)
            {
                state = GameState.StartMenu;
                ToStartMenu();
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
            Game1.self.menu.Activate();
            state = GameState.Paused;
            return true;
        }
        public bool ToStartMenu()
        {
            if (state != GameState.Paused) return false;
            Game1.self.menu.Deactivate();
            Game1.self.starting.Activate();
            state = GameState.StartMenu;
            return true;
        }
        public bool ToLevelSelect()
        {
            if (state == GameState.Paused)
            {
                Game1.self.menu.Deactivate();
            } 
            else if(state == GameState.StartMenu)
            {
                Game1.self.starting.Deactivate();
            }
            else return false;
            
            Game1.self.levelSelect.Activate();

            state = GameState.LevelSelect;
            return true;
        }
        public bool Select(int id)
        {
            if (state != GameState.LevelSelect) return false;
            Game1.self.levelSelect.Deactivate();
            state = GameState.Running;
            Game1.self.activeScene = new();
            Game1.self.activeScene.Initialize(Game1.self.levels.Get(id));
            return true;
        }
        public bool Play()
        {
            if(state == GameState.Paused)
            {
                Game1.self.menu.Deactivate();
            }
            if(state == GameState.StartMenu)
            {
                Game1.self.activeScene = new();
                Game1.self.activeScene.Initialize(Game1.self.levels.Get(level));
                Game1.self.starting.Deactivate();
            }
            state = GameState.Running;
            return true;
        }

    }
}

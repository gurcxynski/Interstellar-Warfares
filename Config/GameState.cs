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
            GameLost,
            Paused,
            StartMenu,
            LevelSelect
        }
        public GameState state;
        public void UpdateStatus()
        {
            // if player has no lives left, lost
            if (Game1.self.activeScene.player.lives < 0)
            {
                if (state == GameState.GameLost && !Game1.self.activeScene.drawScreen)
                {
                    ToStartMenu();
                }
                else
                {
                    GameOver();
                }
            }
            //if there are no enemies left, advance level
            else if (!Game1.self.activeScene.objects.Exists(item => item.GetType().IsSubclassOf(typeof(Enemy))))
            {
                level++;
                //if there are no levels left, won
                if (level >= Game1.self.levels.levels.Count)
                {
                    if (state == GameState.GameWon && !Game1.self.activeScene.drawScreen)
                    {
                        ToStartMenu();
                    }
                    else
                    {
                        GameWon();
                    }
                }
                else
                {
                    Game1.self.activeScene = new();
                    Game1.self.activeScene.Initialize(Game1.self.levels.Get(level));
                }

            }
        }
        public bool GameOver()
        {
            if (state != GameState.Running) return false;

            Game1.self.activeScene.ShowScreen(2000, Game1.self.textures["gameLost"]);

            Game1.self.state.state = GameState.GameLost;
            return true;
        }
        public bool GameWon()
        {
            if (state != GameState.Running) return false;

            Game1.self.activeScene.ShowScreen(2000, Game1.self.textures["gameWon"]);

            Game1.self.state.state = GameState.GameWon;
            return true;
        }
        public bool Pause()
        {
            if (state != GameState.Running) return false;

            Game1.self.menu.Activate();
            Game1.self.activeScene.SmallPauseButton.Deactivate();

            state = GameState.Paused;
            return true;
        }
        public bool ToStartMenu()
        {
            Game1.self.menu.Deactivate();
            Game1.self.activeScene.SmallPauseButton.Deactivate();

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
            else if (state == GameState.StartMenu)
            {
                Game1.self.starting.Deactivate();
            }
            Game1.self.levelSelect.Activate();

            state = GameState.LevelSelect;
            return true;
        }
        // load level with given id
        public bool Select(int id)
        {
            if (state != GameState.LevelSelect) return false;

            Game1.self.state.level = id;
            Game1.self.levelSelect.Deactivate();

            state = GameState.Running;

            Game1.self.activeScene = new();
            Game1.self.activeScene.Initialize(Game1.self.levels.Get(id));
            return true;
        }
        public bool Play()
        {
            if (state == GameState.Paused)
            {
                Game1.self.menu.Deactivate();
                Game1.self.activeScene.SmallPauseButton.Activate();
            }
            if (state == GameState.StartMenu)
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

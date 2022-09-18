using Spaceshooter.Buttons;
using Spaceshooter.Config;
using Spaceshooter.Core;

namespace Spaceshooter.Menus
{
    public class LevelSelect : Menu
    {
        public new void Initialize()
        {
            for (int i = 0; i < Game1.self.levels.levels.Count; i++)
            {
                buttons.Add(new ChooseLevelButton(i));
            }
            buttons.Add(new MusicButton(new(10, Configuration.windowSize.Y - 60)));

            base.Initialize();
        }
    }
}

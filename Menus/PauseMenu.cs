using Spaceshooter.Buttons;
using Spaceshooter.Config;
using Spaceshooter.Core;

namespace Spaceshooter.Menus
{
    public class PauseMenu : Menu
    {
        public new void Initialize()
        {
            buttons.Add(new ResumeButton(1));
            buttons.Add(new LevelSelectButton(2));
            buttons.Add(new QuitToStartButton(3));
            buttons.Add(new MusicButton(new(10, Configuration.windowSize.Y - 60)));
            base.Initialize();
        }
    }
}

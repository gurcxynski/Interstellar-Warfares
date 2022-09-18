using Spaceshooter.Buttons;
using Spaceshooter.Config;
using Spaceshooter.Core;

namespace Spaceshooter.Menus
{
    public class StartScreen : Menu
    {
        public new void Initialize()
        {
            buttons.Add(new PlayButton(1));
            buttons.Add(new LevelSelectButton(2));
            buttons.Add(new ExitGameButton(3));
            buttons.Add(new MusicButton(new(10, Configuration.windowSize.Y - 60)));

            base.Initialize();
        }
    }
}

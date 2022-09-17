using Spaceshooter.Buttons;
using Spaceshooter.Config;
using Spaceshooter.Core;

namespace Spaceshooter.Menus
{
    public class LevelSelect : Menu
    {
        public override void Initialize()
        {
            buttons.Add(new QuitToStartButton(1));
            buttons.Add(new MusicButton(new(10, Configuration.windowSize.Y - 60)));
        }
    }
}

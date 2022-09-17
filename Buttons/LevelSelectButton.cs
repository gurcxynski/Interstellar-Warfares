
using Spaceshooter.Core;

namespace Spaceshooter.Buttons
{
    internal class LevelSelectButton : Button
    {
        public LevelSelectButton(int arg) : base(arg)
        {
            texture = Game1.self.textures["levelselect"];
        }

        protected override void Action()
        {
            Game1.self.state.ToLevelSelect();
        }
    }
}

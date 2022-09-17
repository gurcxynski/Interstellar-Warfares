using Microsoft.Xna.Framework;
using Spaceshooter.Core;

namespace Spaceshooter.Buttons
{
    public class PauseButton : Button
    {
        public PauseButton(Vector2 arg) : base(arg)
        {
            texture = Game1.self.textures["pausebutton"];
        }

        protected override void Action()
        {
            Game1.self.state.Pause();
        }
    }
}

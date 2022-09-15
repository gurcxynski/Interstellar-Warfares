using Microsoft.Xna.Framework;
using Spaceshooter.Core;
namespace Spaceshooter.Buttons
{
    internal class testButton : Button
    {
        public testButton(Vector2 arg) : base(arg)
        {

        }

        protected override void Action()
        {
            Game1.self.DisableMenu();
        }
    }
}

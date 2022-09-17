using Microsoft.Xna.Framework;
using Spaceshooter.Core;
namespace Spaceshooter.Buttons
{
    internal class TestButton : Button
    {
        public TestButton(Vector2 arg) : base(arg)
        {

        }

        protected override void Action()
        {
            Game1.self.state.Play();
        }
    }
}

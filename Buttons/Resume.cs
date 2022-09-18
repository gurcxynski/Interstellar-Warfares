using Spaceshooter.Core;

namespace Spaceshooter.Buttons
{
    internal class ResumeButton : Button
    {
        public ResumeButton(int arg) : base(arg)
        {
            texture = Game1.self.textures["resumebutton"];
        }

        protected override void Action()
        {
            Game1.self.state.Play();
        }
    }
}

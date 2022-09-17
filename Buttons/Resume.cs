using Spaceshooter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

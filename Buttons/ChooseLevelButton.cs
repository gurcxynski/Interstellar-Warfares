using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Spaceshooter.Config;
using Spaceshooter.Core;

namespace Spaceshooter.Buttons
{
    internal class ChooseLevelButton : Button
    {
        int id;
        public ChooseLevelButton(int arg) : base(arg)
        {
            id = arg;
            texture = Game1.self.textures["select" + id];
            position = Game1.self.levelSelect.Position + new Vector2(70 + (arg % 3) * (texture.Width + 30), 250 + (arg / 3) * (texture.Height + 30));
        }

        protected override void Action()
        {
            Game1.self.state.Select(id);
        }
    }
}

using Microsoft.Xna.Framework;
using Spaceshooter.Core;

namespace Spaceshooter.Buttons
{
    internal class MusicButton : Button
    {
        public MusicButton(Vector2 arg) : base(arg)
        {
            texture = Game1.self.textures["turnonmusic"];
        }

        protected override void Action()
        {
            if (Game1.self.instance.Volume != 0)
            {
                Game1.self.instance.Volume = 0;
                texture = Game1.self.textures["turnoffmusic"];
            }
            else
            {
                Game1.self.instance.Volume = 0.1f;
                texture = Game1.self.textures["turnonmusic"];
            }
        }
    }
}

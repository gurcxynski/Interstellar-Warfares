using Microsoft.Xna.Framework;
using Spaceshooter.Config;
using Spaceshooter.Core;
using Spaceshooter.GameObjects;
using System;
using System.Collections.Generic;

namespace Spaceshooter.EnemyTypes
{
    internal class Boss : Enemy
    {
        List<Cannon> cannons = new();

        public Boss(Level level) : base(level)
        {
            Texture = Game1.self.textures["bossnew"];
            HP = Configuration.BossHP;

            // boss has more than one cannon, defined in Configuration.BossLasers

            for (int i = 0; i < Configuration.BossLasers; i++)
            {
                cannons.Add(new(this, new(i * (float)(Texture.Width) / (Configuration.BossLasers - 1), Texture.Height), Configuration.BossShootSpeed, true));
            }

            // generating random movement path for boss, only using upper half of the screen

            Random rnd = new();
            path = new() {
                new(rnd.Next(20, (int)Configuration.windowSize.X - 20), rnd.Next(0, (int)Configuration.windowSize.Y/2 - 50)),
                new(rnd.Next(20, (int)Configuration.windowSize.X - 20), rnd.Next(0, (int)Configuration.windowSize.Y/2 - 50)),
                new(rnd.Next(20, (int)Configuration.windowSize.X - 20), rnd.Next(0, (int)Configuration.windowSize.Y/2 - 50)),
                new(rnd.Next(20, (int)Configuration.windowSize.X - 20), rnd.Next(0, (int)Configuration.windowSize.Y/2 - 50))};
        }
        public override void Update(GameTime UpdateTime)
        {
            cannons.ForEach(delegate (Cannon item) { item.Update(UpdateTime); });

            // boss loses one cannon when HP is 0, dies only if no cannons left

            if (HP == 0 && cannons.Count > 1)
            {
                cannons.RemoveAt(cannons.Count - 1);
                HP = Configuration.BossHP;
            }
            base.Update(UpdateTime);
        }
    }
}

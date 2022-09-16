using Microsoft.Xna.Framework;
using Spaceshooter.Config;
using Spaceshooter.Core;
using Spaceshooter.GameObjects;
using System.Collections.Generic;

namespace Spaceshooter.EnemyTypes
{
    internal class Boss : Enemy
    {
        int phase;
        List<Cannon> cannons = new();
        public Boss(Level level, List<Vector2> patharg) : base(level, patharg)
        {
            Texture = Game1.self.textures["boss"];
            HP = Configuration.BossHP;
            phase = Configuration.BossLasers;
            for(int i = 0; i < Configuration.BossLasers; i++)
            {
                cannons.Add(new(this, new(i * (Texture.Width)/Configuration.BossLasers, Texture.Height), Configuration.BossShootSpeed));
            }
        }
        public new void Update(GameTime UpdateTime)
        {
            cannons.ForEach(delegate (Cannon item) { item.Update(UpdateTime); });
            if (HP == 0 && phase > 0)
            {
                phase--;
                cannons.RemoveAt(phase);
                HP = Configuration.BossHP;
            }
        }
    }
}

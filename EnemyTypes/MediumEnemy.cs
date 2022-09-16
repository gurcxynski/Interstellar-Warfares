using Microsoft.Xna.Framework;
using Spaceshooter.Core;
using Spaceshooter.GameObjects;
using System.Collections.Generic;

namespace Spaceshooter.EnemyTypes
{
    internal class MediumEnemy : Enemy
    {
        public MediumEnemy(Level level, List<Vector2> patharg) : base(level, patharg)
        {
            Texture = Game1.self.textures["enemy2"];
            HP = level.MediumEnemiesHP;
        }
        public MediumEnemy(Level level) : base(level)
        {
            Texture = Game1.self.textures["enemy2"];
            HP = level.MediumEnemiesHP;
        }
    }
}

using Microsoft.Xna.Framework;
using Spaceshooter.Core;
using Spaceshooter.GameObjects;
using System.Collections.Generic;

namespace Spaceshooter.EnemyTypes
{
    internal class EasyEnemy : Enemy
    {
        public EasyEnemy(Level level, List<Vector2> patharg): base(level, patharg)
        {
            Texture = Game1.self.textures["enemy1"];
            HP = level.SimpleEnemiesHP;
        }
        public EasyEnemy(Level level) : base(level)
        {
            Texture = Game1.self.textures["enemy1"];
            HP = level.SimpleEnemiesHP;
        }
    }
}

using Microsoft.Xna.Framework;
using Spaceshooter.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceshooter.EnemyTypes
{
    internal class EasyEnemy : Enemy
    {
        public EasyEnemy(Vector2 arg): base(arg)
        {
            Texture = Game1.self.textures["enemy1"];
            shootingSpeed = 2.5;
            HP = 5;
            Velocity = new Vector2(0, 50);
        }
    }
}

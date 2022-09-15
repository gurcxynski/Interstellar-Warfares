using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceshooter.Config
{
    public class GameState
    {
        public bool paused = false;
        public int level = 0;
        public Vector2 lastVel = Vector2.Zero;
    }
}

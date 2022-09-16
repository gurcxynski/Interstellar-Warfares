using Microsoft.Xna.Framework;

namespace Spaceshooter.Config
{
    public static class Configuration
    {
        public static Vector2 windowSize = new(500, 750);
        public static int basePlayerVel = 30;
        public static Vector2 baseLaserVel = new(0, -600);
        public static float dampening = 0.9f;
        public static int enemySpeed = 100;
    }
}

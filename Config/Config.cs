using Microsoft.Xna.Framework;

namespace Spaceshooter.Config
{
    public static class Configuration
    {
        public static Vector2 windowSize = new(500, 750); //size of game window, do not modify

        // base game stats

        public static int basePlayerVel = 30;
        public static Vector2 baseLaserVel = new(0, -600);
        public static int enemySpeed = 100;

        // player dampening makes movement more natural

        public static float dampening = 0.9f;

        // boss stats

        public static int BossHP = 50;
        public static int BossLasers = 3;
        public static double BossShootSpeed = 0.3;
    }
}

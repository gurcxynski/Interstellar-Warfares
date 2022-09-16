using Microsoft.Xna.Framework;
using Spaceshooter.GameObjects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Spaceshooter.Core
{
    public class Cannon
    {
        Vector2 relative;
        double shootingSpeed;
        public double lastShot = 0;
        GameObject parent;
        public Cannon(GameObject parentArg, Vector2 relativeArg, double speed)
        {
            parent = parentArg;
            relative = relativeArg;
            shootingSpeed = speed;
        }
        public void Update(GameTime UpdateTime)
        {
            if (UpdateTime.TotalGameTime.TotalSeconds - lastShot < shootingSpeed) return;
            lastShot = UpdateTime.TotalGameTime.TotalSeconds;
            Shoot();
        }
        public void Shoot()
        {
            Game1.self.activeScene.toAdd.Add(new Laser(parent.Position + relative, true));
        }
    }
}

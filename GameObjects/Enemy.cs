using Microsoft.Xna.Framework;
using Spaceshooter.Config;
using Spaceshooter.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace Spaceshooter.GameObjects
{
    public abstract class Enemy : GameObject
    {
        public double lastShot = 0;
        public double shootingSpeed = 1;
        List<Vector2> path;
        double lastTurn = 0;
        int onPath = 0;
        public Enemy(Vector2 pos, List<Vector2> patharg)
        {
            Position = pos;
            path = patharg;
        }
        float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
        Vector2 VectorLerp(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }
        public override void Update(GameTime UpdateTime)
        {
            if (lastTurn == 0) lastTurn = UpdateTime.TotalGameTime.TotalSeconds;

            double currentlength = (path[(onPath + 1) % path.Count] - path[onPath]).Length();

            double progress = (UpdateTime.TotalGameTime.TotalSeconds - lastTurn);

            progress *= Configuration.enemySpeed;
            progress /= currentlength;

            if (progress > 1)
            {
                progress = 0;
                onPath = (onPath + 1) % path.Count;
                lastTurn = UpdateTime.TotalGameTime.TotalSeconds;
            }



            Position = VectorLerp(path[onPath], path[(onPath + 1) % path.Count], (float)progress);

            if (UpdateTime.TotalGameTime.TotalSeconds - lastShot < shootingSpeed) return;
            lastShot = UpdateTime.TotalGameTime.TotalSeconds;
            Game1.self.activeScene.toAdd.Add(new Laser(new(Position.X + Texture.Width / 2, Position.Y + Texture.Height), true));

        }
    }
}

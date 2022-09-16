using Microsoft.Xna.Framework;
using Spaceshooter.Config;
using Spaceshooter.Core;
using Spaceshooter.EnemyTypes;
using System;
using System.Collections.Generic;

namespace Spaceshooter.GameObjects
{
    public abstract class Enemy : GameObject
    {

        protected Cannon cannon;

        public double shootingSpeed = 1;
        List<Vector2> path;
        double lastTurn = 0;
        int onPath = 0;
        public Enemy(Level level, List<Vector2> patharg)
        {
            Position = Vector2.Zero;
            path = patharg;
            shootingSpeed = level.EnemyShootingSpeed;
        }
        public Enemy(Level level)
        {
            Position = Vector2.Zero;
            Random rnd = new();
            path = new() {
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y))};
            shootingSpeed = level.EnemyShootingSpeed;
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
            if (cannon is null) cannon = new(this, new(Texture.Width / 2, Texture.Height), shootingSpeed);

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

            if(GetType() != typeof(Boss)) cannon.Update(UpdateTime);

        }
    }
}

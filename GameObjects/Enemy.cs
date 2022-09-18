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

        public double shootingSpeed;

        protected List<Vector2> path;
        protected int currentPath = 0;

        protected double lastTurn = 0;
        public Enemy(Level level, List<Vector2> patharg)
        {
            Position = new(-100, -100); // to hide enemies while loading 
            path = patharg;
            shootingSpeed = level.EnemyShootingSpeed;
        }
        public Enemy(Level level)
        {
            Position = new(-100, -100); // to hide enemies while loading 

            // generating random 4 element path for them to move on

            Random rnd = new();
            path = new() {
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y - 50)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y - 50)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y - 50)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y - 50))};
            shootingSpeed = level.EnemyShootingSpeed;
        }

        // algorithms for calculating position on the looping path

        protected float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
        protected Vector2 VectorLerp(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }
        public override void Update(GameTime UpdateTime)
        {
            if (cannon is null && GetType() != typeof(Boss)) cannon = new(this, new(Texture.Width / 2, Texture.Height), shootingSpeed, true);

            if (lastTurn == 0) lastTurn = UpdateTime.TotalGameTime.TotalSeconds;

            double currentlength = (path[(currentPath + 1) % path.Count] - path[currentPath]).Length();

            double progress = (UpdateTime.TotalGameTime.TotalSeconds - lastTurn);

            // adjusting movement speed to path length

            progress *= Configuration.enemySpeed;
            progress /= currentlength;

            if (progress > 1)
            {
                progress = 0;
                currentPath = (currentPath + 1) % path.Count;
                lastTurn = UpdateTime.TotalGameTime.TotalSeconds;
            }

            Position = VectorLerp(path[currentPath], path[(currentPath + 1) % path.Count], (float)progress);

            // shooting lasers

            if (GetType() != typeof(Boss)) cannon.Update(UpdateTime);

        }
    }
}

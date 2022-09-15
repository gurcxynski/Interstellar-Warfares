using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spaceshooter.Components;
using Spaceshooter.Config;
using System.Collections.Generic;

namespace Spaceshooter.Core
{
    internal class GameObject
    {
        private readonly List<Component> _components = new() { new TextureComponent(), new PositionComponent(), new VelocityComponent() };

        protected T GetComponent<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(T))
                {
                    return component as T;
                }
            }
            return null;
        }
        public Vector2 Velocity { get => GetComponent<VelocityComponent>().Velocity; set => GetComponent<VelocityComponent>().Velocity = value; }
        public Vector2 Position { get => GetComponent<PositionComponent>().Position; set => GetComponent<PositionComponent>().Position = value; }
        public Texture2D Texture { get => GetComponent<TextureComponent>().Texture; set => SetTexture(value); }
        bool SetTexture(Texture2D arg)
        {
            GetComponent<TextureComponent>().Texture = arg;
            return true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
        
        public void Update(float UpdateTime)
        {
            GetComponent<PositionComponent>().Position += Velocity * UpdateTime;
            if (Position.X < 0) Position = new(0, Position.Y);
            else if (Position.X > Configuration.windowSize.X) Position = new(Configuration.windowSize.X, Position.Y);
        }
    }
}

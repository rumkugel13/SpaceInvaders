using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Shared.GameObjects;

namespace SpaceInvaders.Shared.Components
{
    public abstract class BaseComponent
    {
        public CollidableEntity Entity { get; private set; }

        public BaseComponent()
        {
        }

        public void AddTo(CollidableEntity entity)
        {
            this.Entity = entity;
            entity.Add(this);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void Update(float elapsedSeconds)
        {
        }
    }
}

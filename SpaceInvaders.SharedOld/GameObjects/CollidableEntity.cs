using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Shared.Components;
using System.Collections.Generic;

namespace SpaceInvaders.Shared.GameObjects
{
    public class CollidableEntity
    {
        public Vector2 Position;

        public EntityCollection Collection { get; private set; }

        private List<BaseComponent> components;

        public CollidableEntity()
        {
            components = new List<BaseComponent>();
        }

        public void AddTo(EntityCollection entityCollection)
        {
            this.Collection = entityCollection;
            entityCollection.Add(this);
        }

        public void Destroy()
        {
            this.Collection.Remove(this);
            //this.Collection = null;
        }

        public void Add(BaseComponent component)
        {
            this.components.Add(component);
        }

        public T Get<T>() where T : BaseComponent
        {
            foreach (BaseComponent component in this.components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }

            return null;
        }

        public void Remove<T>() where T : BaseComponent
        {
            foreach (BaseComponent component in this.components)
            {
                if (component is T)
                {
                    this.components.Remove(component);
                }
            }
        }

        public virtual void LoadContent(ContentManager content)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (BaseComponent component in this.components)
            {
                component.Draw(spriteBatch);
            }
        }

        public virtual void Update(float elapsedSeconds)
        {
            foreach (BaseComponent component in this.components)
            {
                component.Update(elapsedSeconds);
            }
        }

        public virtual void OnCollision(CollidableEntity other)
        {

        }
    }
}

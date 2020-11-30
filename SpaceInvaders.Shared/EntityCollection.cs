using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Shared.GameObjects;
using System.Collections.Generic;

namespace SpaceInvaders.Shared
{
    public class EntityCollection
    {
        private List<CollidableEntity> entities;

        public EntityCollection()
        {
            this.entities = new List<CollidableEntity>();
        }

        public void Add(CollidableEntity entity)
        {
            this.entities.Add(entity);
        }

        public void Remove(CollidableEntity entity)
        {
            this.entities.Remove(entity);
        }

        public T Get<T>() where T : CollidableEntity
        {
            foreach (CollidableEntity entity in this.entities)
            {
                if (entity is T)
                {
                    return (T)entity;
                }
            }

            return null;
        }

        public IReadOnlyList<T> GetAll<T>() where T : CollidableEntity
        {
            List<T> list = new List<T>();
            foreach (CollidableEntity entity in this.entities)
            {
                if (entity is T)
                {
                    list.Add((T)entity);
                }
            }

            return list;
        }

        public void Clear()
        {
            this.entities.Clear();
        }

        public void Update(float elapsedSeconds)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                CollidableEntity entity = this.entities[i];
                entity.Update(elapsedSeconds);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollidableEntity entity in this.entities)
            {
                entity.Draw(spriteBatch);
            }
        }

        public void LoadContent(ContentManager contentManager)
        {
            foreach (CollidableEntity entity in this.entities)
            {
                entity.LoadContent(contentManager);
            }
        }
    }
}

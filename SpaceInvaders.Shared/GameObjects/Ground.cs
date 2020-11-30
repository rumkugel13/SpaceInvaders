using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Shared.Components;

namespace SpaceInvaders.Shared.GameObjects
{
    public class Ground : CollidableEntity
    {
        private Rectangle visibleRectangle;

        public Ground(Point position, Point size)
        {
            this.visibleRectangle = new Rectangle(position, size);
            new RectangleComponent(position, size).AddTo(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(Brick.DrawTexture, this.visibleRectangle, Color.Gray);
        }
    }
}

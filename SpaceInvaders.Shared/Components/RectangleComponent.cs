using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Shared.GameObjects;

namespace SpaceInvaders.Shared.Components
{
    public class RectangleComponent : BaseComponent
    {
        public Rectangle Bounds;
        public bool ShowBounds;

        public RectangleComponent(Point position, Point size)
        {
            Bounds = new Rectangle(position, size);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (this.ShowBounds)
            {
                spriteBatch.Draw(Brick.DrawTexture, new Rectangle(this.Bounds.Location, new Point(this.Bounds.Width, 1)), Color.Red);
                spriteBatch.Draw(Brick.DrawTexture, new Rectangle(this.Bounds.Location, new Point(1, this.Bounds.Height)), Color.Red);
                spriteBatch.Draw(Brick.DrawTexture, new Rectangle(new Point(this.Bounds.Left, this.Bounds.Bottom), new Point(this.Bounds.Width, 1)), Color.Red);
                spriteBatch.Draw(Brick.DrawTexture, new Rectangle(new Point(this.Bounds.Right, this.Bounds.Top), new Point(1, this.Bounds.Height)), Color.Red);
            }
        }
    }
}

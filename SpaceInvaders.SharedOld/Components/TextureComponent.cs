using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Shared.Components
{
    public class TextureComponent : BaseComponent
    {
        public Texture2D Texture { get; private set; }

        public Vector2 Origin;

        public float LayerDepth;

        public TextureComponent(Texture2D texture)
        {
            this.Texture = texture;
            this.LayerDepth = 1.0f;
            this.Origin = texture.Bounds.Center.ToVector2();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(this.Texture, this.Entity.Position, null, Color.White, 0f, this.Origin, 1f, SpriteEffects.None, this.LayerDepth);
        }
    }
}

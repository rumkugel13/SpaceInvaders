using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Shared
{
    public class TextField
    {
        public string Text 
        { 
            get => this.text;
            set
            {
                if (value != this.text)
                {
                    this.text = value;
                    if (this.rightToLeft)
                    {
                        this.Origin = new Point((int)this.Font.MeasureString(this.Text).X, 0);
                    }
                    else if (this.centered)
                    {
                        this.Origin = (this.Font.MeasureString(this.Text) / 2).ToPoint();
                    }
                }
            }
        }

        public SpriteFont Font { get; private set; }
        public Point Position { get; set; }
        public Point Origin { get; set; }

        private string text;
        private bool rightToLeft;
        private bool centered;

        public TextField(SpriteFont font, Point position, string text, bool rightToLeft = false)
        {
            this.Font = font;
            this.Position = position;
            this.rightToLeft = rightToLeft;
            this.Text = text;
        }

        public void SetOriginToCenter()
        {
            this.centered = true;
            this.Origin = new Point((int)(this.Font.MeasureString(this.Text).X / 2), (int)(this.Font.MeasureString(this.Text).Y / 2));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.Font, this.Text, this.Position.ToVector2(), Color.White, 0f, this.Origin.ToVector2(), 1f, SpriteEffects.None, 1f);
        }
    }
}

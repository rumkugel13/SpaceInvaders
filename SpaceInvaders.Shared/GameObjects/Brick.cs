using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Shared.Components;

namespace SpaceInvaders.Shared.GameObjects
{
    public class Brick : CollidableEntity
    {
        public static Texture2D DrawTexture;

        private Rectangle visibleRectangle;

        public Brick(Point position, Point size)
        {
            this.visibleRectangle = new Rectangle(position, size);
            new RectangleComponent(position, size).AddTo(this);
        }

        public override void OnCollision(CollidableEntity other)
        {
            base.OnCollision(other);
            
            this.Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(Brick.DrawTexture, this.visibleRectangle, Color.White);
        }

        public static void CreateBunkers(EntityCollection collection, Texture2D template)
        {
            //Color[] rawData = new Color[template.Width * template.Height];
            //template.GetData<Color>(rawData);

            // Note that this stores the pixel's row in the first index, and the pixel's column in the second,
            // with this setup.
            Color[,] rawDataAsGrid = new Color[template.Height, template.Width];
            for (int row = 0; row < template.Height; row++)
            {
                for (int column = 0; column < template.Width; column++)
                {
                    // Assumes row major ordering of the array.
                    rawDataAsGrid[row, column] = bunker[row][column] == '.' ? Color.Black : Color.White;// rawData[row * template.Width + column];
                }
            }

            int amount = 4;
            int y = 350;

            int bricksize = 4;
            int width = template.Width * bricksize;
            int xstart = (960 - (amount * width)) / (amount + 1);
            int xincrement = width + xstart;
            int xcurrent = xstart;

            for (int i = 0; i < amount; i++)    // create all 4
            {
                for (int j = 0; j < template.Height; j++)
                {
                    for (int k = 0; k < template.Width; k++)
                    {
                        if (rawDataAsGrid[j, k] == Color.White)
                        {
                            new Brick(new Point(xcurrent + k * bricksize, y + j * bricksize), new Point(bricksize)).AddTo(collection);
                        }
                    }
                }
                xcurrent += xincrement;
            }
        }

        private static string[] bunker =
        {
            ".......##########.......",
            "....################....",
            "..####################..",
            ".######################.",
            ".######################.",
            "########################",
            "########################",
            "##########....##########",
            "#########......#########",
            "########........########",
            "########........########",
            "########........########",
        };
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Shared.Components;

namespace SpaceInvaders.Shared.GameObjects
{
    public class Projectile : CollidableEntity
    {
        public static Texture2D Texture;
        public bool MovingUp { get; private set; }

        public Projectile(Vector2 position, bool up)
        {
            this.Position = position;
            this.MovingUp = up;
            new TextureComponent(Texture).AddTo(this);
            new MotionComponent(Vector2.UnitY * 500 * (up ? -1 : +1)).AddTo(this);
            new RectangleComponent((this.Position - new Vector2(2, 8)).ToPoint(), new Point(4, 16)).AddTo(this);
        }

        public override void OnCollision(CollidableEntity other)
        {
            base.OnCollision(other);

            if (MovingUp)
            {
                // moving up only when shot from groundcannon
                this.Collection.Get<GroundCannon>().CanShoot = true;
                this.Destroy();
            }
            else if (!(other is Alien || other is Mothership))
            {
                this.Destroy();
            }
        }

        public override void Update(float elapsedSeconds)
        {
            base.Update(elapsedSeconds);

            this.Get<RectangleComponent>().Bounds.Location = (this.Position - new Vector2(2, 8)).ToPoint();

            // check out of bounds
            if (this.Position.Y < 0 || this.Position.Y > 540)
            {
                if (MovingUp)
                {
                    this.Collection.Get<GroundCannon>().CanShoot = true;
                }

                this.Destroy();
            }
        }
    }
}

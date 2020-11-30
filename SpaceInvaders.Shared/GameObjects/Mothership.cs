using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Shared.Components;

namespace SpaceInvaders.Shared.GameObjects
{
    public class Mothership : CollidableEntity
    {
        private readonly float MaxPos = 1500;

        public Mothership()
        {
            this.Position = new Vector2(-this.MaxPos, 100);
            new MotionComponent(new Vector2(150, 0)).AddTo(this);
            new RectangleComponent((this.Position - new Vector2(32, 16)).ToPoint(), new Point(64, 32)).AddTo(this);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            new TextureComponent(content.Load<Texture2D>("Textures/Mothership_64_AA")).AddTo(this);
        }

        public override void OnCollision(CollidableEntity other)
        {
            base.OnCollision(other);

            if (other is Projectile p && p.MovingUp)
            {
                this.Collection.Get<GroundCannon>().Get<ScoreComponent>().Score += 100;
                MotionComponent motion = this.Get<MotionComponent>();
                motion.Velocity *= -1.1f;   //reverse and speed up
                this.Position = new Vector2(-this.MaxPos * System.Math.Sign(motion.Velocity.X), this.Position.Y);
            }
        }

        public override void Update(float elapsedSeconds)
        {
            base.Update(elapsedSeconds);

            this.Get<RectangleComponent>().Bounds.Location = (this.Position - new Vector2(32, 16)).ToPoint();

            if (this.Position.X < 0 + -this.MaxPos || this.Position.X > 960 + this.MaxPos)
            {
                this.Get<MotionComponent>().Velocity *= -1.0f;    //reverse
            }
        }
    }
}

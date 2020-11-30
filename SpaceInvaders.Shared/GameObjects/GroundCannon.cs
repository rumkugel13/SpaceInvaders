using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Shared.Components;

namespace SpaceInvaders.Shared.GameObjects
{
    public class GroundCannon : CollidableEntity
    {
        private const int Reserves = 3;
        private const int Velocity = 200;

        public bool CanShoot = true;
        private KeyboardState oldKeyboardState;

        public GroundCannon(Vector2 position)
        {
            this.Position = position;
            new MotionComponent().AddTo(this);
            new ScoreComponent().AddTo(this);
            new ReservesComponent(Reserves).AddTo(this);
            new RectangleComponent((this.Position - new Vector2(32, 16)).ToPoint(), new Point(64, 32)).AddTo(this);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            new TextureComponent(content.Load<Texture2D>("Textures/Cannon_64_AA")).AddTo(this);
        }

        public override void OnCollision(CollidableEntity other)
        {
            base.OnCollision(other);

            // note: optionally check type of other gameobject for more specific reaction if using different layers
            if (this.Get<ReservesComponent>().Reserves-- > 0)
            {
                // just respawn for now
                this.Position = new Vector2(480, 440);
            }
            else
            {
                this.Destroy();
            }
        }

        public override void Update(float elapsedSeconds)
        {
            base.Update(elapsedSeconds);

            KeyboardState keyboard = Keyboard.GetState();

            this.Get<MotionComponent>().Velocity = Vector2.UnitX *
              ((keyboard.IsKeyDown(Keys.Left) ? -Velocity : 0) +
              (keyboard.IsKeyDown(Keys.Right) ? +Velocity : 0));

            if (this.Position.X < 0 || this.Position.X > 960)
            {
                this.Position = new Vector2(MathHelper.Clamp(this.Position.X, 0, 960), this.Position.Y);
            }

            this.Get<RectangleComponent>().Bounds.Location = (this.Position - new Vector2(32, 16)).ToPoint();

            if (this.CanShoot && keyboard.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space))
            {
                new Projectile(this.Position + Vector2.UnitY * -20, true).AddTo(this.Collection);
                this.CanShoot = false;
            }

            oldKeyboardState = keyboard;
        }
    }
}
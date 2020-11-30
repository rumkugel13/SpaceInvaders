using Microsoft.Xna.Framework;

namespace SpaceInvaders.Shared.Components
{
    public class MotionComponent : BaseComponent
    {
        public Vector2 Velocity;

        public MotionComponent(Vector2 velocity)
        {
            this.Velocity = velocity;
        }

        public MotionComponent()
        {
        }

        public override void Update(float elapsedSeconds)
        {
            base.Update(elapsedSeconds);

            this.Entity.Position += this.Velocity * elapsedSeconds;
        }
    }
}

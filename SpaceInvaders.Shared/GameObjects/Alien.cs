using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Shared.Components;
using System;

namespace SpaceInvaders.Shared.GameObjects
{
    public class Alien : CollidableEntity
    {
        public static Texture2D TextureSmall;
        public static Texture2D TextureMedium;
        public static Texture2D TextureLarge;

        private Size size;
        private float projectileSpawnTime;
        private float maxProjectileTimespan = 20;

        private static Random rand = new Random();
        private static Vector2 velocity = Vector2.UnitX * 100;

        public enum Size
        {
            Small, Medium, Large
        }

        public Alien(Vector2 position, Size size)
        {
            this.Position = position;
            this.size = size;
            new MotionComponent(velocity).AddTo(this);
            new RectangleComponent((this.Position - Vector2.One * (size == Size.Small ? 12 : size == Size.Medium ? 14 : 16)).ToPoint(),
                        new Point((size == Size.Small ? 24 : size == Size.Medium ? 28 : 32))).AddTo(this);
            new TextureComponent(size == Size.Small ? TextureSmall : size == Size.Medium ? TextureMedium : TextureLarge).AddTo(this);

            this.ResetSpawnTime();
        }

        public override void OnCollision(CollidableEntity other)
        {
            base.OnCollision(other);

            if (other is Projectile p && p.MovingUp)
            {
                this.Collection.Get<GroundCannon>().Get<ScoreComponent>().Score += size == Size.Small ? 30 : size == Size.Medium ? 20 : 10;
                this.Destroy();
            }
        }

        public override void Update(float elapsedSeconds)
        {
            base.Update(elapsedSeconds);

            this.Get<RectangleComponent>().Bounds.Location = (this.Position - Vector2.One * (size == Size.Small ? 12 : size == Size.Medium ? 14 : 16)).ToPoint();

            this.projectileSpawnTime -= elapsedSeconds;

            if (this.projectileSpawnTime < 0)
            {
                new Projectile(this.Position + Vector2.UnitY * 20, false).AddTo(this.Collection);

                this.ResetSpawnTime();
            }
        }

        private void ResetSpawnTime()
        {
            this.projectileSpawnTime = (float)rand.NextDouble() * this.maxProjectileTimespan;
        }
    }
}

using Microsoft.Xna.Framework;
using SpaceInvaders.Shared.Components;
using SpaceInvaders.Shared.GameObjects;

namespace SpaceInvaders.Shared
{
    public class AlienWave
    {
        public bool GameOver { get; private set; } = false;

        public int CurrentWave { get; private set; } = 0;

        private float velocity = 100;
        private float direction = 1;
        private readonly int maxWave = 10;

        private EntityCollection collection;

        public AlienWave(EntityCollection collection)
        {
            this.collection = collection;
        }

        public void Reset()
        {
            this.velocity = 100;
            this.direction = 1;
            this.CurrentWave = 0;
            this.GameOver = false;
        }

        public void Update()
        {
            bool noneLeft = true;
            foreach (Alien a in this.collection.GetAll<Alien>())
            {
                noneLeft = false;
                // if any alien is out of bounds
                if (a.Position.X < 0 || a.Position.X > 960)
                {
                    this.SwitchDirection();
                    break;
                }

                if (a.Position.Y > 540 - 100)
                {
                    // alien touched ground
                    this.GameOver = true;
                    return;
                }
            }

            if (noneLeft)
            {
                // all aliens gone
                if (this.CurrentWave < this.maxWave)
                {
                    this.velocity *= 1.1f;
                    this.AddAlienWave();
                }
                else
                {
                    // game won
                    this.GameOver = true;
                    return;
                }
            }
        }

        public void AddAlienWave()
        {
            this.CurrentWave++;
            this.direction = 1;

            Alien.Size currentSize = Alien.Size.Small;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    new Alien(new Vector2((j + 1) * 40, (i + 1) * 40), currentSize).AddTo(this.collection);
                }
                currentSize++;
            }
        }

        private void SwitchDirection()
        {
            this.direction *= -1;

            foreach (Alien a in this.collection.GetAll<Alien>())
            {
                a.Get<MotionComponent>().Velocity.X = this.velocity * this.direction;
                a.Position = new Vector2(a.Position.X, a.Position.Y + 10);
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Shared.Components;
using SpaceInvaders.Shared.GameObjects;
using System;

namespace SpaceInvaders.Shared.Scenes
{
    public class MainGameScene : BaseScene
    {
        private TextField txtScore, txtReserves, txtWave, txtGun, txtOverlay;

        private EntityCollection collection;

        private GameState gameState = GameState.Playing;
        private AlienWave alienWave;
        private bool debugDraw;

        public MainGameScene(Game game) : base(game)
        {
            this.switcher.AddScene(this);

            this.collection = new EntityCollection();
            this.alienWave = new AlienWave(this.collection);

            Brick.DrawTexture = this.Content.Load<Texture2D>("Textures/Pixel");
            Projectile.Texture = this.Content.Load<Texture2D>("Textures/Projectile_16");
            Alien.TextureSmall = this.Content.Load<Texture2D>("Textures/Alien_24_AA");
            Alien.TextureMedium = this.Content.Load<Texture2D>("Textures/Alien_28_AA");
            Alien.TextureLarge = this.Content.Load<Texture2D>("Textures/Alien_32_AA");

            this.CreateGui();
        }

        enum GameState
        {
            Playing, Paused, GameOver
        }

        private void CreateGui()
        {
            SpriteFont labelFont = this.Content.Load<SpriteFont>("Fonts/Arial16");

            this.txtScore = new TextField(labelFont, new Point((960 / 2) - 200, 5), "Score: 0");
            this.txtReserves = new TextField(labelFont, new Point((960 / 2) + 100, 5), "Reserves: 3");
            this.txtWave = new TextField(labelFont, new Point(5, 5), "Wave: 1");
            this.txtGun = new TextField(labelFont, new Point(960 - 5, 5), "Ready", true);

            SpriteFont largeFont = this.Content.Load<SpriteFont>("Fonts/Arial24");

            this.txtOverlay = new TextField(largeFont, new Point(480, 270), "Paused");
            this.txtOverlay.SetOriginToCenter();
        }

        protected override void OnEnabledChanged(object sender, EventArgs args)
        {
            base.OnEnabledChanged(sender, args);

            if (this.Enabled)
            {
                this.AddGameobjects();
            }
            else
            {
                this.collection?.Clear();
                this.SetPaused(false);
            }
        }

        //protected override void OnEnter()
        //{
        //    base.OnEnter();

        //    this.AddGameobjects();
        //}

        //protected override void OnExit()
        //{
        //    base.OnExit();

        //    this.collection.Clear();
        //    this.SetPaused(false);
        //}

        private void SetPaused(bool value)
        {
            this.gameState = value ? GameState.Paused : GameState.Playing;
            this.Game.IsMouseVisible = value;
        }

        private void ChangeState(GameState gameState)
        {
            this.gameState = gameState;
            switch (gameState)
            {
                case GameState.Paused:
                    this.txtOverlay.Text = "Paused";
                    this.SetPaused(true);
                    break;
                case GameState.Playing:
                    this.SetPaused(false);
                    break;
                case GameState.GameOver:
                    this.txtOverlay.Text = "Game Over!\n" + this.txtScore.Text;
                    break;
            }
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.OnDraw(gameTime, spriteBatch);

            this.collection.Draw(spriteBatch);

            this.txtScore.Draw(spriteBatch);
            this.txtReserves.Draw(spriteBatch);
            this.txtWave.Draw(spriteBatch);
            this.txtGun.Draw(spriteBatch);

            if (this.gameState == GameState.Paused || this.gameState == GameState.GameOver)
            {
                spriteBatch.Draw(Brick.DrawTexture, new Rectangle(Point.Zero, new Point(960, 540)), Color.Black * 0.7f);
                this.txtOverlay.Draw(spriteBatch);
            }
        }

        protected override void OnUpdate(float elapsedSeconds)
        {
            switch (this.gameState)
            {
                case GameState.Playing:
                    {
                        if (this.OnKeyDown(Keys.Escape))
                        {
                            this.ChangeState(GameState.GameOver);
                        }

                        if (this.OnKeyDown(Keys.P))
                        {
                            this.ChangeState(GameState.Paused);
                        }

                        if (this.OnKeyDown(Keys.R))
                        {
                            this.Reload();
                        }

                        // note: only works on currently active entities, not for example projectiles
                        if (this.OnKeyDown(Keys.F1))
                        {
                            this.debugDraw = !this.debugDraw;

                            foreach (var o in this.collection.GetAll<CollidableEntity>())
                            {
                                if (o.Get<RectangleComponent>() != null)
                                {
                                    o.Get<RectangleComponent>().ShowBounds = this.debugDraw;
                                }
                            }
                        }

                        this.collection.Update(elapsedSeconds);

                        this.alienWave.Update();
                        if (this.alienWave.GameOver)
                        {
                            this.ChangeState(GameState.GameOver);
                        }

                        this.DoCollisionChecks();

                        GroundCannon cannon = this.collection.Get<GroundCannon>();
                        if (cannon == null)
                        {
                            // all reserves used
                            this.ChangeState(GameState.GameOver);
                        }
                        else
                        {
                            this.txtScore.Text = "Score: " + cannon.Get<ScoreComponent>().Score;
                            this.txtReserves.Text = "Reserves: " + cannon.Get<ReservesComponent>().Reserves;
                            this.txtWave.Text = "Wave: " + this.alienWave.CurrentWave;
                            this.txtGun.Text = cannon.CanShoot ? "Ready" : "Reloading";
                        }
                    }
                    break;
                case GameState.Paused:
                    if (this.OnKeyDown(Keys.Escape))
                    {
                        this.ChangeState(GameState.GameOver);
                    }

                    if (this.OnKeyDown(Keys.R))
                    {
                        this.Reload();
                    }

                    if (this.OnKeyDown(Keys.P))
                    {
                        this.ChangeState(GameState.Playing);
                    }
                    break;
                case GameState.GameOver:
                    if (this.OnKeyDown(Keys.Escape))
                    {
                        SwitchScene<MainMenuScene>();
                        return;
                    }
                    break;
            }
        }

        private void Reload()
        {
            this.collection.Clear();
            this.SetPaused(false);
            this.AddGameobjects();
        }

        private void DoCollisionChecks()
        {
            var projectiles = this.collection.GetAll<Projectile>();
            var gameObjects = this.collection.GetAll<CollidableEntity>();

            foreach (Projectile projectile in projectiles)
            {
                foreach (CollidableEntity collidable in gameObjects)
                {
                    if (collidable != projectile)
                    {
                        if (projectile.Get<RectangleComponent>().Bounds.Intersects(collidable.Get<RectangleComponent>().Bounds))
                        {
                            projectile.OnCollision(collidable);
                            collidable.OnCollision(projectile);
                        }
                    }
                }
            }
        }

        private void AddGameobjects()
        {
            new Ground(new Point(0, 440), new Point(960, 100)).AddTo(this.collection);
            new GroundCannon(new Vector2(480, 440)).AddTo(this.collection);

            this.alienWave.Reset();
            this.alienWave.AddAlienWave();

            Brick.CreateBunkers(this.collection, this.Content.Load<Texture2D>("Textures/DefensiveBunker_24"));

            new Mothership().AddTo(this.collection);

            foreach (CollidableEntity entity in this.collection.GetAll<CollidableEntity>())
            {
                entity.LoadContent(this.Content);
            }
        }
    }
}

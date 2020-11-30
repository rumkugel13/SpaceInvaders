using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Shared
{
    public class BaseScene : DrawableGameComponent
    {
        protected SceneSwitcher switcher;
        protected ContentManager Content;

        private SpriteBatch SpriteBatch;
        private KeyboardState oldKeyboardState, newKeyboardState;

        public BaseScene(Game game) : base(game)
        {
            this.Content = new ContentManager(game.Services, "Content");
            this.switcher = game.Services.GetService<SceneSwitcher>();
            this.Enabled = false;
            this.Visible = false;
        }

        public override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();

            this.OnDraw(gameTime, this.SpriteBatch);

            this.SpriteBatch.End();
        }

        protected virtual void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public override void Initialize()
        {
            this.SpriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            this.oldKeyboardState = this.newKeyboardState;
            this.newKeyboardState = Keyboard.GetState();

            this.OnUpdate((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        protected virtual void OnUpdate(float elapsedSeconds)
        {

        }

        protected void SwitchScene<T>() where T : BaseScene
        {
            this.switcher.SwitchScene<T>();
        }

        protected bool OnKeyDown(Keys key)
        {
            return this.oldKeyboardState.IsKeyUp(key) && this.newKeyboardState.IsKeyDown(key);
        }

        protected bool OnKeyUp(Keys key)
        {
            return this.newKeyboardState.IsKeyUp(key) && this.oldKeyboardState.IsKeyDown(key);
        }
    }
}

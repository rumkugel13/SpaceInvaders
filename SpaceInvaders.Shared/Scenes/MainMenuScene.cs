using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceInvaders.Shared.Scenes
{
    public class MainMenuScene : BaseScene
    {
        private TextField txtPlay, txtExit, txtHeadline, txtSelection, txtControls;
        private bool justEnabled;

        public MainMenuScene(Game game) : base(game)
        {
            this.switcher.AddScene(this);
            this.CreateGui();
        }

        protected override void OnEnabledChanged(object sender, EventArgs args)
        {
            base.OnEnabledChanged(sender, args);

            if (Enabled)
            {
                justEnabled = true;
            }
        }

        protected override void OnUpdate(float elapsedSeconds)
        {
            // for some reason, the keyboard state is weird after scene has just been enabled, 
            // justEnabled works around that
            if (!justEnabled && this.OnKeyDown(Keys.Escape))
            {
                this.Game.Exit();
                return;
            }

            if (this.OnKeyDown(Keys.Down))
            {
                // move down
                this.txtSelection.Position = this.txtExit.Position;
            }

            if (this.OnKeyDown(Keys.Up))
            {
                // move up
                this.txtSelection.Position = this.txtPlay.Position;
            }

            if (this.OnKeyDown(Keys.Enter))
            {
                if (this.txtSelection.Position == this.txtPlay.Position)
                {
                    SwitchScene<MainGameScene>();
                    return;
                }
                else if (this.txtSelection.Position == this.txtExit.Position)
                {
                    this.Game.Exit();
                    return;
                }
            }

            justEnabled = false;
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.OnDraw(gameTime, spriteBatch);

            this.txtHeadline.Draw(spriteBatch);
            this.txtControls.Draw(spriteBatch);
            this.txtPlay.Draw(spriteBatch);
            this.txtExit.Draw(spriteBatch);
            this.txtSelection.Draw(spriteBatch);
        }

        private void CreateGui()
        {
            SpriteFont largeFont = this.Content.Load<SpriteFont>("Fonts/Arial24");

            this.txtHeadline = new TextField(largeFont, new Point(480, 100), "Space Invaders");
            this.txtHeadline.SetOriginToCenter();
            this.txtPlay = new TextField(largeFont, new Point(680, 270 - 30), "Play");
            this.txtPlay.SetOriginToCenter();
            this.txtExit = new TextField(largeFont, new Point(680, 270 + 30), "Exit");
            this.txtExit.SetOriginToCenter();

            this.txtSelection = new TextField(largeFont, this.txtPlay.Position, "---          ---");
            this.txtSelection.SetOriginToCenter();

            this.txtControls = new TextField(largeFont, new Point(150, 150),
                "Controls:\n" +
                "   Left/Right to move\n" +
                "   Space to shoot\n" +
                "   R to reset\n" +
                "   P to pause\n" +
                "   Esc to end game");
        }
    }
}

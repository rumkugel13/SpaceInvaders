using SpaceInvaders.Shared;
using System;
using System.IO;
using static Retyped.dom;

namespace SpaceInvaders.Web
{
    public class App
    {
        private static Game1 _game;
        
        public static void Main()
        {
            var canvas = new HTMLCanvasElement();
            canvas.width = 960;
            canvas.height = 540;
            canvas.id = "monogamecanvas";

            var button = new HTMLButtonElement();
            button.innerHTML = "Run Game";

            document.body.appendChild(canvas);
            document.body.appendChild(button);

            button.onclick = (ev) =>
            {
                _game = new Game1();
                _game.Run();
            };
        }
    }
}

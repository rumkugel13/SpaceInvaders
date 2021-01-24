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
            document.body.setAttribute("style", "margin: 0; overflow: hidden;");

            var div = new HTMLDivElement();
            div.setAttribute("style", "width: 960px; height: 540px;");

            var button = new HTMLButtonElement();
            button.innerHTML = "Run Game";
            button.setAttribute("style", "width: 100%; height: 100%; background-color: rgb(100, 149, 237); color: rgb(255, 255, 255); font-size: 20px; border: 0;");

            document.body.appendChild(div);
            div.appendChild(button);

            button.onclick = (ev) =>
            {
                var canvas = new HTMLCanvasElement();
                canvas.width = 960;
                canvas.height = 540;
                canvas.id = "monogamecanvas";

                div.removeChild(button);
                div.appendChild(canvas);

                _game = new Game1();
                _game.Run();

                canvas.focus();
            };
        }
    }
}

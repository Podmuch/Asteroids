using UnityEngine;
using Asteroids.Controller;

namespace Asteroids.Button
{
    public class PlayButtonController : AbstractController
    {
        public Texture2D normalButton;
        public Texture2D hoverButton;

        private void Awake()
        {
            Vector2 size = new Vector2(Screen.width * 0.4f, Screen.height * 0.3f);
            Vector2 margin = new Vector2(Screen.width * 0.5f, Screen.height * 0.6f);
            model = new ButtonModel(() => { Application.LoadLevel(1); });
            view = new ButtonView(margin, size, normalButton, hoverButton);
        }
    }
}
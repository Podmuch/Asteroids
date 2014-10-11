//Menu Controller
//  create menu model and model view
using UnityEngine;

namespace Asteroids.Menu
{
    public class MenuController : AbstractController
    {
        public Texture2D sound;
        public Texture2D notSound;
        public Texture2D normalButton;
        public Texture2D hoverButton;
        public Transform backgroundImage;
        public Texture2D Title;
        private void Awake()
        {
// ReSharper disable ConvertToLambdaExpression
            model = new MenuModel(() => { Application.LoadLevel(1); });
// ReSharper restore ConvertToLambdaExpression
            view = new MenuView(normalButton, hoverButton, sound, notSound, backgroundImage, Title);
        }
    }
}
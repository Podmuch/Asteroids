using UnityEngine;
using System.Timers;
using Asteroids.View;

namespace Asteroids.MovableObject
{
    public class MovableObjectView : AbstractView
    {
        private Texture2D[] textureArray;
        private Renderer renderer;
        private int currentTexture;
        private Timer textureChangeRateTimer;
        private bool isChangeTextureAvailable;

        public MovableObjectView(Renderer _renderer, Texture2D[] _textureArray)
        {
            renderer = _renderer;
            textureArray = _textureArray;
            currentTexture = 0;
            textureChangeRateTimer = new Timer(50);
            textureChangeRateTimer.Elapsed += TextureRate;
            textureChangeRateTimer.Start();
        }

        public void ResetView(Texture2D[] _textureArray)
        {
            textureArray = _textureArray;
            currentTexture = 0;
            textureChangeRateTimer.Interval = 50;
        }
        private void TextureRate(object sender, ElapsedEventArgs e)
        {
            isChangeTextureAvailable = true;
        }

        public override bool Draw(System.Object drawParams)
        {
            bool returnValue = false;
            if (isChangeTextureAvailable)
            {
                if (drawParams != null)
                {
                    textureArray = drawParams as Texture2D[];
                    currentTexture = 0;
                    textureChangeRateTimer.Interval = 30;
                    drawParams = null;
                    returnValue = true;
                }
                renderer.material.mainTexture=textureArray[currentTexture];
                currentTexture = (currentTexture + 1) % textureArray.Length;
                isChangeTextureAvailable = false;
            }
            return returnValue;
        }
    }
}

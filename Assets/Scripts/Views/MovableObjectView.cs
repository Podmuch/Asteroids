using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
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

        private void TextureRate(object sender, ElapsedEventArgs e)
        {
            isChangeTextureAvailable = true;
        }

        public override void Draw(System.Object drawParams)
        {
            if (isChangeTextureAvailable)
            {
                if (drawParams != null)
                {
                    textureArray = drawParams as Texture2D[];
                    currentTexture = 0;
                    textureChangeRateTimer.Interval = 30;
                }
                renderer.material.mainTexture=textureArray[currentTexture];
                currentTexture = (currentTexture + 1) % textureArray.Length;
                isChangeTextureAvailable = false;
            }
        }
    }
}

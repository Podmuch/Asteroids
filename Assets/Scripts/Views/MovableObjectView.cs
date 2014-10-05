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
        private float lastChangeTexture;
        private float changeTextureRate;
        private bool isChangeTextureAvailable
        {
            get
            {
                return Time.realtimeSinceStartup - lastChangeTexture > changeTextureRate;
            }
        }

        public MovableObjectView(Renderer _renderer, Texture2D[] _textureArray)
        {
            renderer = _renderer;
            textureArray = _textureArray;
            currentTexture = 0;
            changeTextureRate = 0.05f;
        }

        public void ResetView(Texture2D[] _textureArray)
        {
            textureArray = _textureArray;
            currentTexture = 0;
            changeTextureRate = 0.05f;
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
                    changeTextureRate = 0.03f;
                    drawParams = null;
                    returnValue = true;
                }
                renderer.material.mainTexture=textureArray[currentTexture];
                currentTexture = (currentTexture + 1) % textureArray.Length;
                lastChangeTexture = Time.realtimeSinceStartup;
            }
            return returnValue;
        }
    }
}

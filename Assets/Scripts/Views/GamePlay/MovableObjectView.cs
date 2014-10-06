//MovableObject view (the same for all movable Objects)
//  getting parameters from model and change textures in renderer <-animation
using UnityEngine;
using System.Timers;

namespace Asteroids.MovableObject
{
    //inherits from base abstract class for all Views (drawing)
    public class MovableObjectView : AbstractView
    {
        //textures <- initial is object textures but when he is destroyed model sends explosion textures
        private Texture2D[] textureArray;
        private Renderer renderer;
        private int currentTexture;
        //allows count the period between the shoots
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
        //reset view (for player)
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
                //if params are null object isn't destroyed;
                if (drawParams != null)
                {
                    textureArray = drawParams as Texture2D[];
                    currentTexture = 0;
                    //faster animation (more textures)
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

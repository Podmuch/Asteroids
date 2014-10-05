using UnityEngine;
using System.Timers;
using Asteroids.Controller;

namespace Asteroids.MovableObject
{
    public abstract class MovableObjectController : AbstractController
    {
        public Texture2D[] textureArray;
        private bool isMoveActive
        {
            get
            {
                return Time.realtimeSinceStartup - lastMove > 0.01f;
            }
        }
        private float lastMove { get; set; }
        protected virtual void Awake()
        {
            view = new MovableObjectView(renderer, textureArray);
        }

        protected virtual void Update()
        {
            if (isMoveActive&&model!=null)
            {
                (model as MovableObjectModel).Move();
                lastMove = Time.realtimeSinceStartup;
                if (view.Draw(model.DrawParams))
                    model.DrawParams = null;
            }
        }
        protected override void OnGUI()
        {
            
        }
    }
}

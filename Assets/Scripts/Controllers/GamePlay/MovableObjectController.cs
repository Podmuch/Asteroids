//MovableObject Controller
//  base abstract controller for all movableObjects
using UnityEngine;
using System.Timers;

namespace Asteroids.MovableObject
{
    //inherits from base abstract class for all Controllers (drawing)
    public abstract class MovableObjectController : AbstractController
    {
        //texture array for animations
        public Texture2D[] textureArray;
        //dependence of movement and time
        private bool isMoveActive
        {
            get
            {
                return Time.realtimeSinceStartup - lastMove > 0.01f;
            }
        }
        //allows count the period between the moves
        private float lastMove { get; set; }
        protected virtual void Awake()
        {
            view = new MovableObjectView(renderer, textureArray);
        }

        //movement and drawing (Abstract Controller draw in OnGUI function)
        // these objects don't need do it on gui
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

using UnityEngine;
using Asteroids.Model;
using Asteroids.View;

namespace Asteroids.Controller
{
    public abstract class AbstractController : MonoBehaviour
    {
        public AbstractModel model;
        protected AbstractView view;

        protected virtual void OnGUI()
        {
            if (model != null)
                if (view.Draw(model.DrawParams))
                    model.DrawParams = null;
        }
    }
}

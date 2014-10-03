using UnityEngine;

namespace Asteroids.View
{
    public abstract class AbstractView
    {
        protected Vector2 size;
        protected Vector2 margin;
        protected GUIStyle style;

        public abstract bool Draw(System.Object drawParams);
    }
}

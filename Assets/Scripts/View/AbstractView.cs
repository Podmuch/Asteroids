using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids.View
{
    public abstract class AbstractView
    {
        protected Vector2 size;
        protected Vector2 margin;
        protected GUIStyle style;

        public abstract void Draw(System.Object drawParams);
    }
}

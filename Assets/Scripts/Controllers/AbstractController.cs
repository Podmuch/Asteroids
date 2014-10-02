using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.Model;
using Asteroids.View;

namespace Asteroids.Controller
{
    public abstract class AbstractController : MonoBehaviour
    {
        protected AbstractModel model;
        protected AbstractView view;
        protected void OnGUI()
        {
            if(model!=null)
                view.Draw(model.DrawParams);
        }
    }
}

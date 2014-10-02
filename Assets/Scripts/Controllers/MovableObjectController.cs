using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Asteroids.Controller;

namespace Asteroids.MovableObject
{
    public abstract class MovableObjectController : AbstractController
    {
        public Texture2D[] textureArray;
        private Timer moveRateTimer;
        private bool isMoveActive;
        protected virtual void Awake()
        {
            view = new MovableObjectView(renderer, textureArray);
            moveRateTimer = new Timer();
            moveRateTimer.Interval = 10;
            moveRateTimer.Elapsed += MoveRate;
            moveRateTimer.Start();
        }

        private void MoveRate(object sender, ElapsedEventArgs e)
        {
            isMoveActive = true;
        }

        protected virtual void Update()
        {
            if (isMoveActive)
            {
                (model as MovableObjectModel).Move();
                isMoveActive = false;
            }
        }
    }
}

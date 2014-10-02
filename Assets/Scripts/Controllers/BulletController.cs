using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.View.Explosion;
using System.Timers;

namespace Asteroids.MovableObject.Bullet
{
    public class BulletController : MovableObjectController
    {
        public bool isBlue;
        protected override void Awake()
        {
            base.Awake();
            model = new BulletModel(transform);
        }

        protected override void Update()
        {
            base.Update();
            if ((model as BulletModel).range < 0)
                Destroy(gameObject);     
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            (model as BulletModel).range = -1;
        }
    }
}

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.Controller;

namespace Asteroids.MovableObject.Player
{
    public class PlayerController : MovableObjectController
    {
        protected override void Awake()
        {
            base.Awake();
            model = new PlayerModel(transform);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}

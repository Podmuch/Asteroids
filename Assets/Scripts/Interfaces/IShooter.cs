using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Asteroids.MovableObject.Bullet;

namespace Asteroids.Interface
{
    public interface IShooter
    {
        bool isShoot {get;}
        void ShootRate(object sender, ElapsedEventArgs e);
        void Shoot();
    }
}

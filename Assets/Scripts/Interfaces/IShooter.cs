using UnityEngine;
using System.Timers;

namespace Asteroids.Interface
{
    public interface IShooter
    {
        bool isShoot {get;}
        void ShootRate(object sender, ElapsedEventArgs e);
        void Shoot();
    }
}

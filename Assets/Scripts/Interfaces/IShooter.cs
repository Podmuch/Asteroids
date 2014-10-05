using UnityEngine;
using System.Timers;

namespace Asteroids.Interface
{
    public interface IShooter
    {
        float lastShoot { get; set; }
        bool isShoot {get;}
        void Shoot();
    }
}

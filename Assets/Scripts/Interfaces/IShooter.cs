//Interface for shooters (controller Interface)
//  contains last shoot time, flag for shooting and shoot method
using UnityEngine;

namespace Asteroids.Interface
{
    public interface IShooter
    {
        float lastShoot { get; set; }
        bool isShoot {get;}
        void Shoot();
    }
}

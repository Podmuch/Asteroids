using Asteroids.Controller;
namespace Asteroids.Interface
{
    interface IBullet : IDestructible
    {
        float Range { get; set; }
        AbstractController Owner { get; set; }
    }
}

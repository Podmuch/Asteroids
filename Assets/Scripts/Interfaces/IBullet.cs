using Asteroids.Controller;
namespace Asteroids.Interface
{
    interface IBullet : IDestructible
    {
        AbstractController Owner { get; set; }
    }
}

namespace Asteroids.Interface
{
    interface IEnemy : IDestructible
    {
        int Points { get; set; }
    }
}

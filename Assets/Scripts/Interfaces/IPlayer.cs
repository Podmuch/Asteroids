namespace Asteroids.Interface
{
    public interface IPlayer : IDestructible
    {
        int Score{get;set;}
        bool isUntouchable { get; set; }
    }
}

using UnityEngine;

namespace Asteroids.Interface
{
    public interface IDestructible
    {
        int Lives { get; set; }
        bool isDestroyed { get; set; }
        void Destruct(Texture2D[] explosionTextureArray);
    }
}

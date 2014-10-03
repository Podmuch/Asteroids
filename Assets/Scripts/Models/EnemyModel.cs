using UnityEngine;
using Asteroids.Interface;

namespace Asteroids.MovableObject.Enemy
{
    public abstract class EnemyModel : MovableObjectModel, IEnemy
    {
        public bool isDestroyed { get; set; }
        public int Points { get; set; }
        public int Lives { get; set; }

        public void Destruct(Texture2D[] explosionTextureArray)
        {
            DrawParams = explosionTextureArray;
            isDestroyed = true;
            Lives--;
            speed = Vector2.zero;
        }
        public override void Move()
        {
            //Update position
            objectTransform.position += (Vector3)speed;
            Wrapping();
        }
    }
}

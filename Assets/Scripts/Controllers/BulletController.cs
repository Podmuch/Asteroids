using UnityEngine;
using Asteroids.Interface;
using Asteroids.View.Explosion;

namespace Asteroids.MovableObject.Bullet
{
    public class BulletController : MovableObjectController
    {
        private StaticExplosion explosion;
        protected override void Awake()
        {
            base.Awake();
            model = new BulletModel(transform);
            explosion = FindObjectOfType<StaticExplosion>();
        }

        protected override void Update()
        {
            base.Update();
            if ((model as BulletModel).range < 0)
                Death();     
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Death();
        }

        private void Death() 
        {
            (model as BulletModel).Destruct((model as BulletModel).Owner.model as IPlayer != null ? explosion.blueExplosion : explosion.redExplosion);
            Destroy(gameObject, 0.5f);
        }
    }
}

using UnityEngine;
using System.Timers;
using Asteroids.Controller;
using Asteroids.Interface;
using Asteroids.View.Explosion;

namespace Asteroids.MovableObject.Player
{
    public class PlayerController : MovableObjectController, IShooter
    {
        public Transform bullet;
        private Timer ShootRateTimer;
        private bool isShootActive;
        private Timer DyingAnimationTimer;
        private StaticExplosion explosion;
        protected override void Awake()
        {
            base.Awake();
            model = new PlayerModel(transform);
            isShootActive = false;
            explosion = FindObjectOfType<StaticExplosion>();
            ShootRateTimer = new Timer(500);
            ShootRateTimer.Elapsed += ShootRate;
            ShootRateTimer.Start();
            DyingAnimationTimer = new Timer(500);
            DyingAnimationTimer.Elapsed += AnimationTime;
        }

        protected override void Update()
        {
            base.Update();
            Shoot();
        }
        public bool isShoot
        {
            get
            {
                return Input.GetKey(KeyCode.Space)&&isShootActive&&!(model as PlayerModel).isDestroyed;
            }
        }
        public void Shoot()
        {
            if(isShoot)
            {
                Transform bulletPointer=(Transform)Instantiate(bullet, transform.position, transform.rotation);
                (bulletPointer.GetComponent<AbstractController>().model as IBullet).Owner = this;
                bulletPointer.Translate(0, 0.5f, 0);
                isShootActive = false;
            }
        }

        public void ShootRate(object sender, ElapsedEventArgs e)
        {
            isShootActive = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!(model as PlayerModel).isDestroyed)
            {
                switch (other.tag)
                {
                    case "Enemy":
                        if (!(other.GetComponent<AbstractController>().model as IEnemy).isDestroyed)
                            Death();
                        break;
                    case "Bullet":
                        if ((other.GetComponent<AbstractController>().model as IBullet).Owner.model as IPlayer==null&&
                            !(other.GetComponent<AbstractController>().model as IBullet).isDestroyed)
                            Death();
                        break;
                }
            }
        }
        private void Death()
        {
            (model as PlayerModel).Destruct(explosion.redExplosion);
            DyingAnimationTimer.Start();
        }

        private void AnimationTime(object sender, ElapsedEventArgs e)
        {
            (model as PlayerModel).stopMove = false;
            (view as MovableObjectView).ResetView(textureArray);
            (model as PlayerModel).isDestroyed = false;
            DyingAnimationTimer.Stop();  
        }
    }
}

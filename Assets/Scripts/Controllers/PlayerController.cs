using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Asteroids.Controller;
using Asteroids.Interface;
using Asteroids.MovableObject.Bullet;
using Asteroids.View.Explosion;
using Asteroids.MovableObject.Asteroid;

namespace Asteroids.MovableObject.Player
{
    public class PlayerController : MovableObjectController, IShooter
    {
        public Transform bullet;
        private Timer ShootRateTimer;
        private bool isShootActive;
        private bool isDestroyed;
        private Timer DyingAnimationTimer;
        private bool isAnimationEnd = false;
        private StaticExplosion explosion;
        protected override void Awake()
        {
            base.Awake();
            model = new PlayerModel(transform);
            isShootActive = false;
            isDestroyed = false;
            explosion = FindObjectOfType<StaticExplosion>();
            ShootRateTimer = new Timer();
            ShootRateTimer.Interval = 500;
            ShootRateTimer.Elapsed += ShootRate;
            ShootRateTimer.Start();
            DyingAnimationTimer = new Timer();
            DyingAnimationTimer.Interval = 500;
            DyingAnimationTimer.Elapsed += AnimationTime;
        }

        protected override void Update()
        {
            base.Update();
            Shoot();
            if (isAnimationEnd)
            {
                view = new MovableObjectView(renderer, textureArray);
                DyingAnimationTimer.Stop();
                isDestroyed = false;
                isAnimationEnd = false;
            }
        }
        public bool isShoot
        {
            get
            {
                return Input.GetKey(KeyCode.Space);
            }
        }
        public void Shoot()
        {
            if(isShoot&&isShootActive)
            {
                Transform bulletPointer=(Transform)Instantiate(bullet, transform.position, transform.rotation);
                bulletPointer.Translate(0, 0.5f, 0);
                isShootActive = false;
            }
        }

        public void ShootRate(object sender, ElapsedEventArgs e)
        {
            isShootActive = true;
        }

        public void AddPoints(int points) 
        {
            (model as PlayerModel).score += points;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isDestroyed)
            {
                switch (other.tag)
                {
                    case "Enemy":
                        if (!other.GetComponent<AsteroidController>().isDestroyed)
                            Death();
                        break;
                    case "Bullet":
                        if (!other.GetComponent<BulletController>().isBlue)
                            Death();
                        break;
                }
            }
        }
        private void Death()
        {
            (model as PlayerModel).Destruct(explosion.redExplosion);
            DyingAnimationTimer.Start();
            (model as PlayerModel).Lives--;
            (model as PlayerModel).stopMove = true;
        }

        private void AnimationTime(object sender, ElapsedEventArgs e)
        {
            isAnimationEnd = true;
            (model as PlayerModel).stopMove = false;
        }
    }
}

using UnityEngine;
using Asteroids.View.Explosion;
using Asteroids.Controller;
using Asteroids.Interface;
using System.Timers;
using Asteroids.GamePlay;
using Asteroids.Model.Sound;

namespace Asteroids.MovableObject.Enemy.EnemyShip
{
    class EnemyShipController : MovableObjectController, IShooter
    {
        public Transform bullet=null;
        private StaticExplosion explosion;
        private StaticSound sound;
        private Timer ShootRateTimer;
        private bool isShootActive;

        public void AddModel(int lives)
        {
            model = new EnemyShipModel(transform, lives);
            explosion = FindObjectOfType<StaticExplosion>();
            sound = FindObjectOfType<StaticSound>();
            isShootActive = false;
            ShootRateTimer = new Timer(500);
            ShootRateTimer.Elapsed += ShootRate;
            ShootRateTimer.Start();
        }

        protected override void Update()
        {
            if ((model as EnemyShipModel) == null) AddModel(2);
            base.Update();
            Shoot();
        }

        public bool isShoot
        {
            get
            {
                return isShootActive && !(model as EnemyShipModel).isDestroyed;
            }
        }

        public void Shoot()
        {
            if (isShoot)
            {
                if (sound.sounds[3].isPlaying)
                    sound.sounds[3].Stop();
                sound.sounds[3].Play();
                Quaternion bulletRotation = transform.rotation;
                bulletRotation.eulerAngles += new Vector3(0, 0, Random.Range(-45, 45));
                Transform bulletPointer = (Transform)Instantiate(bullet, transform.position, bulletRotation);
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
            if (!(model as EnemyShipModel).isDestroyed)
            {
                switch (other.tag)
                {
                    case "Bullet":
                        IBullet bulletPointer = other.GetComponent<AbstractController>().model as IBullet;
                        IPlayer bulletOwner = bulletPointer.Owner.model as IPlayer;
                        if ((bulletOwner != null || GamePlayController.EnemyFriendlyFire) && !bulletPointer.isDestroyed)
                        {
                            bulletPointer.isDestroyed = true;
                            if (bulletOwner != null)
                                bulletOwner.Score += ((model as EnemyShipModel).Points * (model as EnemyShipModel).Lives);
                            (model as EnemyShipModel).Destruct(bulletOwner != null ? explosion.blueExplosion : explosion.redExplosion);
                            if ((model as EnemyShipModel).Lives < 1)
                            {
                                Destroy(gameObject, 1);
                                GamePlayController.NumberOfEnemyShipsInGame--;
                            }
                        }
                        break;
                }
            }
        }
    }
}

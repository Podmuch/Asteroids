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

        public void AddModel(int lives)
        {
            model = new EnemyShipModel(transform, lives);
            explosion = FindObjectOfType<StaticExplosion>();
            sound = FindObjectOfType<StaticSound>();
        }

        protected override void Update()
        {
            if ((model as EnemyShipModel) == null) AddModel(2);
            base.Update();
            Shoot();
        }
        public float lastShoot { get; set; }
        public bool isShoot
        {
            get
            {
                return !(model as EnemyShipModel).isDestroyed && Time.realtimeSinceStartup - lastShoot > 0.5f;
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
                lastShoot = Time.realtimeSinceStartup;
            }
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

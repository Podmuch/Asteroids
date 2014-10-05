using UnityEngine;
using System.Timers;
using Asteroids.Controller;
using Asteroids.Interface;
using Asteroids.View.Explosion;
using Asteroids.GamePlay;
using Asteroids.Model.Sound;

namespace Asteroids.MovableObject.Player
{
    public class PlayerController : MovableObjectController, IShooter
    {
        public Transform bullet;
        private Timer DyingAnimationTimer;
        private StaticExplosion explosion;
        private StaticSound sound;
        private float UntouchableTime;
        private float UntouchableTimeMax;
        private int lastScoreInLiveBonus;
        protected override void Awake()
        {
            base.Awake();
            model = new PlayerModel(transform);
            explosion = FindObjectOfType<StaticExplosion>();
            sound = FindObjectOfType<StaticSound>();
            DyingAnimationTimer = new Timer(500);
            DyingAnimationTimer.Elapsed += AnimationTime;
            UntouchableTimeMax = 5.00f;
            UntouchableTime = UntouchableTimeMax+0.01f;
            lastScoreInLiveBonus = 0;
        }

        protected override void Update()
        {
            if ((model as PlayerModel).Lives > 0)
            {
                base.Update();
                Shoot();
                Untouchable();
                BonusLive();
            }
        }
        public float lastShoot { get; set; }
        public bool isShoot
        {
            get
            {
                return !(model as PlayerModel).isDestroyed && Time.realtimeSinceStartup-lastShoot> 0.5f;
            }
        }
        public void Shoot()
        {
            if(isShoot)
            {
                if (sound.sounds[2].isPlaying)
                    sound.sounds[2].Stop();
                sound.sounds[2].Play();
                Transform bulletPointer=(Transform)Instantiate(bullet, transform.position, transform.rotation);
                (bulletPointer.GetComponent<AbstractController>().model as IBullet).Owner = this;
                bulletPointer.Translate(0, 0.5f, 0);
                lastShoot = Time.realtimeSinceStartup;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!(model as PlayerModel).isDestroyed&&!(model as PlayerModel).isUntouchable)
            {
                switch (other.tag)
                {
                    case "Enemy":
                        if (!(other.GetComponent<AbstractController>().model as IEnemy).isDestroyed)
                            Death();
                        break;
                    case "Bullet":
                        IBullet bulletPointer = other.GetComponent<AbstractController>().model as IBullet;
                        if ((bulletPointer.Owner.model as IPlayer == null || GamePlayController.PlayerFriendlyFire) &&!bulletPointer.isDestroyed)
                            Death();
                        break;
                }
            }
        }

        private void BonusLive()
        {
            if ((model as PlayerModel).Score - lastScoreInLiveBonus > 10000)
            {
                lastScoreInLiveBonus += 10000;
                (model as PlayerModel).Lives++;
            }
        }

        private void Untouchable()
        {
            if (UntouchableTime > 0)
            {
                if (UntouchableTime > UntouchableTimeMax)
                    transform.position = Vector3.zero;
                UntouchableTime -= Time.deltaTime;
                if (UntouchableTime - Mathf.Floor(UntouchableTime) > 0.5f)
                    renderer.material.color -= new Color(0.5f, 0.5f, 0.5f, 0.0f);
                else
                    renderer.material.color += new Color(0.5f, 0.5f, 0.5f, 0.0f);
            }
            else
                if((model as PlayerModel).isUntouchable)
                    (model as PlayerModel).isUntouchable = false;
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
            UntouchableTime = UntouchableTimeMax + 0.01f;
            (model as PlayerModel).isUntouchable = true;
        }
    }
}

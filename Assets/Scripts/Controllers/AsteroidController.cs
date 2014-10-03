using UnityEngine;
using Asteroids.View.Explosion;
using Asteroids.Controller;
using Asteroids.Interface;

namespace Asteroids.MovableObject.Enemy.Asteroid
{
    public class AsteroidController : MovableObjectController
    {
        
        private StaticExplosion explosion;

        public void AddModel(int lives)
        {
            model= new AsteroidModel(transform, lives);
            explosion = FindObjectOfType<StaticExplosion>();
        }

        protected override void Update()
        {
            if ((model as AsteroidModel) == null) AddModel(3);
            base.Update();
        }
        private void Fragmentation(int fragments)
        {
            if((model as AsteroidModel).Lives>1)
            {
                for(int i=0;i<fragments;i++)
                {
                    Transform pieceOfAsteroid = (Transform)Instantiate(transform, transform.position, transform.rotation);
                    pieceOfAsteroid.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                    pieceOfAsteroid.transform.localScale = new Vector3(1, 1, 1);
                    pieceOfAsteroid.gameObject.GetComponent<AsteroidController>().AddModel((model as AsteroidModel).Lives);  
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!(model as AsteroidModel).isDestroyed)
            {
                switch (other.tag)
                {
                    case "Bullet":
                        IPlayer bulletOwner = (other.GetComponent<AbstractController>().model as IBullet).Owner.model as IPlayer;
                        if (bulletOwner != null && !(other.GetComponent<AbstractController>().model as IBullet).isDestroyed)
                        {
                            (other.GetComponent<AbstractController>().model as IBullet).isDestroyed = true;
                            bulletOwner.Score += ((model as AsteroidModel).Points * (model as AsteroidModel).Lives);
                            (model as AsteroidModel).Destruct(bulletOwner != null ? explosion.blueExplosion : explosion.redExplosion);
                            Fragmentation(2);
                            Destroy(gameObject, 1);
                        }
                        break;
                }
            }
        }
    }
}

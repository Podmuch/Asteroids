using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Asteroids.View.Explosion;
using Asteroids.MovableObject.Bullet;
using Asteroids.MovableObject.Player;

namespace Asteroids.MovableObject.Asteroid
{
    public class AsteroidController : MovableObjectController
    {
        public bool isDestroyed = false;
        private int points;
        private StaticExplosion explosion;

        public void AddModel(int lives)
        {
            model= new AsteroidModel(transform, lives);
            explosion = FindObjectOfType<StaticExplosion>();
            points = 100;
        }
        protected override void Update()
        {
            if (model == null) AddModel(3); //tmp
            base.Update();            
        }

        private void Fragmentation(int fragments)
        {
            if((model as AsteroidModel).Lives>1)
            {
                for(int i=0;i<fragments;i++)
                {
                    Transform pieceOfAsteroid = (Transform)Instantiate(transform, transform.position, transform.rotation);
                    pieceOfAsteroid.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
                    pieceOfAsteroid.transform.localScale = new Vector3(1, 1, 1);
                    pieceOfAsteroid.gameObject.GetComponent<AsteroidController>().AddModel((model as AsteroidModel).Lives-1);
                    
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isDestroyed)
            {
                switch (other.tag)
                {
                    case "Bullet":
                        bool bluecolor = other.GetComponent<BulletController>().isBlue;
                        (model as AsteroidModel).Destruct(bluecolor ? explosion.blueExplosion : explosion.redExplosion);
                        Fragmentation(2);
                        FindObjectOfType<PlayerController>().AddPoints(points * (model as AsteroidModel).Lives);
                        Destroy(gameObject, 1);
                        isDestroyed = true;
                        break;
                }
            }
        }
        
    }
}

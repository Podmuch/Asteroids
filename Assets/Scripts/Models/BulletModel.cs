using UnityEngine;
using Asteroids.Controller;
using Asteroids.Interface;

namespace Asteroids.MovableObject.Bullet
{
    public class BulletModel : MovableObjectModel, IBullet
    {
        public float Range { get; set; }
        public int Lives { get; set; }
        public bool isDestroyed { get; set; }
        public AbstractController Owner { get; set; }


        public BulletModel(Transform _objectTransform) 
        {
            Range = 10;
            objectTransform = _objectTransform;
            int acuteAngle = Mathf.RoundToInt(objectTransform.eulerAngles.z) % 90;
            speed = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (90 - acuteAngle)),
                                                 Mathf.Cos(Mathf.Deg2Rad * acuteAngle));
            maxSpeed=0.1f;
            speed = CorrectSpeedDirection(speed);
            speed=speed.normalized*maxSpeed;
        }

        public override void Move()
        {  
            //Update position
            objectTransform.position += (Vector3)speed;
            Range -= speed.magnitude;
            Wrapping();
        }
        public void Destruct(Texture2D[] explosionTextureArray) 
        {
            DrawParams = explosionTextureArray;
            speed = Vector2.zero;
        }
    }
}

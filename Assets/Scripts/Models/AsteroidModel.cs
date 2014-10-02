using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.Interface;

namespace Asteroids.MovableObject.Asteroid
{
    public class AsteroidModel : MovableObjectModel, IDestructible
    {
        public int Lives { get; set; }
        public override object DrawParams
        {
            get
            {
                return _drawParams;
            }
        }
        private object _drawParams = null;

        public AsteroidModel(Transform _objectTransform, int _lives) 
        {
            Lives = _lives;
            _objectTransform.localScale *= Lives;
            objectTransform = _objectTransform;
            int acuteAngle = Mathf.RoundToInt(objectTransform.eulerAngles.z) % 90;
            speed = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (90 - acuteAngle)),
                                                 Mathf.Cos(Mathf.Deg2Rad * acuteAngle));
            maxSpeed=0.03f;
            speed = CorrectSpeedDirection(speed);
            speed=speed.normalized*maxSpeed;
        }

        public override void Move()
        {  
            //Update position
            objectTransform.position += (Vector3)speed;
            Wrapping();
        }
        public void Destruct(Texture2D[] explosionTextureArray)
        {
            _drawParams = explosionTextureArray;
            speed = Vector2.zero;
        }
    }
}

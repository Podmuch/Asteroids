using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.Interface;

namespace Asteroids.MovableObject.Bullet
{
    public class BulletModel : MovableObjectModel
    {
        public float range;
        public override object DrawParams
        {
            get
            {
                return null;
            }
        }

        public BulletModel(Transform _objectTransform) 
        {
            range = 15;
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
            range -= speed.magnitude;
            Wrapping();
        }
    }
}

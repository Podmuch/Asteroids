using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.Interface;

namespace Asteroids.MovableObject.Player
{
    public class PlayerModel : MovableObjectModel, IDestructible
    {
        public int Lives{get;set;}
        public int score {get;set;}
        public bool stopMove { get; set; }

        public override object DrawParams
        {
            get 
            {
                if(_drawParams==null)
                    return _drawParams;
                object tmp = _drawParams;
                _drawParams = null;
                return tmp;

            }
        }
        private object _drawParams = null;

        private float deltaRotate;
        private bool isTurnLeft
        {
            get
            {
                return Input.GetKey(KeyCode.LeftArrow);
            }
        }
        private bool isTurnRight
        {
            get
            {
                return Input.GetKey(KeyCode.RightArrow);
            }
        }
        private bool isAccelerate
        {
            get
            {
                return Input.GetKey(KeyCode.UpArrow);
            }
        }

        public PlayerModel(Transform _objectTransform)
        {
            Lives = 3;
            score = 0;
            objectTransform = _objectTransform;
            speed = Vector2.zero;
            maxSpeed = 0.05f;
            acceleration = 0.001f;
            deltaRotate = 3;
        }

        public override void Move()
        {
            if (stopMove)
                return;
            //Update Rotation if player is pressing key
            Vector3 rotation = objectTransform.rotation.eulerAngles;
            if (isTurnLeft ^ isTurnRight)
            {
                rotation.z += (isTurnLeft)?deltaRotate:-deltaRotate;
                objectTransform.rotation = Quaternion.Euler(rotation);
            }
            //Update Speed
            if (isAccelerate)
            {          
                int acuteAngle = Mathf.RoundToInt(rotation.z) % 90;
                Vector2 deltaSpeed = new Vector2(Mathf.Cos(Mathf.Deg2Rad* (90 - acuteAngle)) * acceleration,
                                                 Mathf.Cos(Mathf.Deg2Rad * acuteAngle) * acceleration);
                deltaSpeed=CorrectSpeedDirection(deltaSpeed);
                if ((speed + deltaSpeed).magnitude < maxSpeed)
                    speed += deltaSpeed;
                else
                    speed =(deltaSpeed.normalized+speed.normalized).normalized* maxSpeed;
            }
            //Update position
            objectTransform.position += (Vector3)speed;
            Wrapping();
        }
        public void Destruct(Texture2D[] explosionTextureArray)
        {
            _drawParams = explosionTextureArray;
            speed = Vector2.zero;
            Lives--;
        }
    }
}

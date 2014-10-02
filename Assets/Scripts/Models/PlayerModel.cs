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
        public int score;

        public override object DrawParams
        {
            get 
            {
                return _drawParams;
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
        private bool isShoot
        {
            get
            {
                return Input.GetKey(KeyCode.Space);
            }
        }
        private int whichQuarter(float rotation)
        {
            return Mathf.RoundToInt(rotation) / 90;
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
                float swapTmp;
                switch (whichQuarter(rotation.z))
                {
                    // 0 | 3
                    // -----  Number of Quarter
                    // 1 | 2
                    case 2:
                        deltaSpeed.y = -deltaSpeed.y;
                        break;
                    case 3:
                        swapTmp= deltaSpeed.x;
                        deltaSpeed.x = deltaSpeed.y;
                        deltaSpeed.y = swapTmp;
                        break;
                    case 0:
                        deltaSpeed.x = -deltaSpeed.x;
                        break;
                    case 1:
                        swapTmp = -deltaSpeed.x;
                        deltaSpeed.x =-deltaSpeed.y;
                        deltaSpeed.y = swapTmp;
                        break;
                }
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
        }
    }
}

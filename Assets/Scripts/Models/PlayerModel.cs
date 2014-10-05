using UnityEngine;
using Asteroids.Interface;

namespace Asteroids.MovableObject.Player
{
    public class PlayerModel : MovableObjectModel, IPlayer
    {
        public bool isDestroyed { get; set; }
        public int Lives { get; set; }
        public int Score { get; set; }
        public bool isUntouchable { get; set; }
        public bool stopMove { get; set; }

        private float mapWidth = 25;
        private float mapHeight = 10;
        private float maxDistance = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f).magnitude;
        private float currentDistance;
        private float acuteAngle;
        private float Angle
        {
            get
            {
                Vector2 touchPosition = (Input.touchCount > 0) ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;
                touchPosition.x = touchPosition.x * mapWidth / Screen.width - mapWidth * 0.5f;
                touchPosition.y = touchPosition.y * mapHeight / Screen.height - mapHeight * 0.5f;
                touchPosition.x -= objectTransform.position.x;
                touchPosition.y -= objectTransform.position.y;
                currentDistance = touchPosition.magnitude;
                return ComputeAngle(touchPosition);
            }
        }
        private float ComputeAngle(Vector2 Distance)
        {
            int Quarter;
            acuteAngle = Mathf.Rad2Deg;
            if (Distance.x < 0 && Distance.y < 0)
                Quarter = 1;
            else
            {
                if (Distance.x > 0 && Distance.y > 0)
                    Quarter = 3;
                else
                {
                    if (Distance.x > 0)
                        Quarter = 2;
                    else
                        Quarter = 0;
                }
            }
            if (Quarter % 2 == 0)
                acuteAngle *= Mathf.Acos(Mathf.Abs(Distance.y) / currentDistance);
            else
                acuteAngle *= Mathf.Acos(Mathf.Abs(Distance.x) / currentDistance);
            return acuteAngle + 90 * Quarter;
        }

        private float CurrentAcceleration
        {
            get
            {
                return (maxDistance < currentDistance) ? acceleration : acceleration * currentDistance / maxDistance;
            }
        }
        private bool isTapped
        {
            get
            {
                return Input.touchCount > 0 || Input.mousePresent;
            }
        }

        public PlayerModel(Transform _objectTransform)
        {
            isUntouchable = true;
            isDestroyed = false;
            Lives = 20;
            Score = 0;
            objectTransform = _objectTransform;
            speed = Vector2.zero;
            maxSpeed = 0.05f;
            acceleration = 0.001f;
        }

        public override void Move()
        {
            if (stopMove)
                return;
            if (isTapped)
            {
                //Update Rotation if player is pressing key
                objectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, Angle));
                //Update Speed       
                Vector2 deltaSpeed = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (90 - acuteAngle)) * acceleration,
                                                    Mathf.Cos(Mathf.Deg2Rad * acuteAngle) * acceleration);
                deltaSpeed = CorrectSpeedDirection(deltaSpeed);
                if ((speed + deltaSpeed).magnitude < maxSpeed)
                    speed += deltaSpeed;
                else
                    speed = (deltaSpeed.normalized + speed.normalized).normalized * maxSpeed;
                //Update position
            }
            objectTransform.position += (Vector3)speed;
            Wrapping();
        }
        public void Destruct(Texture2D[] explosionTextureArray)
        {
            DrawParams = explosionTextureArray;
            speed = Vector2.zero;
            isDestroyed = true;
            stopMove = true;
            Lives--;
        }
    }
}

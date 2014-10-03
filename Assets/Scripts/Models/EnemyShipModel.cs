using UnityEngine;

namespace Asteroids.MovableObject.Enemy.EnemyShip
{
    class EnemyShipModel : EnemyModel
    {
        public EnemyShipModel(Transform _objectTransform, int _lives) 
        {
            isDestroyed = false;
            Lives = _lives;
            Points = 200;
            _objectTransform.localScale *= 0.5f*Lives;
            objectTransform = _objectTransform;
            int acuteAngle = Mathf.RoundToInt(objectTransform.eulerAngles.z) % 90;
            speed = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (90 - acuteAngle)),
                                                 Mathf.Cos(Mathf.Deg2Rad * acuteAngle));
            maxSpeed=0.03f;
            speed = CorrectSpeedDirection(speed);
            speed=speed.normalized*maxSpeed;
        }

        new public void Destruct(Texture2D[] explosionTextureArray)
        {
            if (Lives == 1)
                base.Destruct(explosionTextureArray);
            else
            {
                Lives--;
                objectTransform.localScale *= (float)Lives / (Lives + 1.0f);
            }
        }
    }
}

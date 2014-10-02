using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.Model;

namespace Asteroids.MovableObject
{
    public abstract class MovableObjectModel : AbstractModel
    {
        protected Transform objectTransform;
        protected Vector2 speed;
        protected float maxSpeed;
        protected float acceleration;

        private bool isOutofLeft
        {
            get
            {
                return objectTransform.position.x < leftTopCorner.x;
            }
        }
        private bool isOutofRight
        {
            get
            {
                return objectTransform.position.x > rightBottomCorner.x;
            }
        }
        private bool isOutofTop
        {
            get
            {
                return objectTransform.position.y > leftTopCorner.y;
            }
        }
        private bool isOutofBottom
        {
            get
            {
                return objectTransform.position.y < rightBottomCorner.y;
            }
        }
        private static Vector2 leftTopCorner= new Vector2(-12.2f,5.2f);
        private static Vector2 rightBottomCorner= new Vector2(12.2f,-5.2f);

        protected void Wrapping()
        {
            Vector3 currentPosition= objectTransform.position;
            if (isOutofLeft) currentPosition.x = rightBottomCorner.x - 0.2f;
            if (isOutofRight) currentPosition.x = leftTopCorner.x + 0.2f;
            if (isOutofTop) currentPosition.y = rightBottomCorner.y + 0.2f;
            if (isOutofBottom) currentPosition.y = leftTopCorner.y - 0.2f;
            objectTransform.position = currentPosition;
        }

        public abstract void Move();
    }
}

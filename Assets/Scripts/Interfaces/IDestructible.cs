using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.Model;

namespace Asteroids.Interface
{
    public interface IDestructible
    {
        int Lives { get; set; }
        void Destruct(Texture2D[] explosionTextureArray);
    }
}

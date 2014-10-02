using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids.Interface
{
    public interface IDestructible
    {
        //po smierci musze stworzyc nowy view
        int Lives { get; set; }
        void Destruct(Texture2D[] explosionTextureArray);
    }
}

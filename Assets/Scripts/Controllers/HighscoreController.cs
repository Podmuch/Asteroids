using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.Controller;

namespace Asteroids.Highscore
{
    public class HighscoreController : AbstractController
    {
        private void Awake()
        {
            model = new HighscoreModel();
            view = new HighscoreView();
        }
    }
}

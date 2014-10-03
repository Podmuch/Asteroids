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

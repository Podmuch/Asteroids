//Static Sounds
using UnityEngine;

namespace Asteroids.Static
{
    public class StaticSound : MonoBehaviour
    {
        public bool isSoundOn;
        public AudioSource[] sounds;

        private void Awake()
        {
            isSoundOn = PlayerPrefs.GetInt("sound") == 1;
            //soundtrack
            if (isSoundOn)
                sounds[0].Play();
            else
                sounds[0].Stop();
            //alternative soundtrack <-not used
            sounds[1].Stop();
            //player shoot
            sounds[2].Stop();
            //enemy shoot
            sounds[3].Stop();
        }
    }
}

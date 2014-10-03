using UnityEngine;

namespace Asteroids.Model.Sound
{
    public class StaticSound : MonoBehaviour
    {
        public AudioSource[] sounds;

        private void Awake()
        {
            sounds[0].Play();
            sounds[1].Stop();
            sounds[2].Stop();
            sounds[3].Stop();
        }
    }
}

using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        [SerializeField] private AudioSource soundObject;
        [SerializeField] private GameObject player;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }
        
        public static void PlaySound(AudioClip clip, Transform position, float volume = 0.6f)
        {
            if (_instance == null)
            {
                return;
            }
            
            AudioSource audioSource = Instantiate(_instance.soundObject, position.position, Quaternion.identity);
            
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
            
            float clipLenght = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLenght);
        }

        public static void PlaySoundAtPlayer(AudioClip clip, float volume = 0.6f)
        {
            PlaySound(clip, _instance.player.transform, volume);
        }
    }
}
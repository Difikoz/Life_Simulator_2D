using UnityEngine;

namespace WinterUniverse
{
    public class PawnSoundComponent : PawnComponent
    {
        private AudioSource _audioSource;

        public override void Initialize()
        {
            base.Initialize();
            _audioSource = GetComponentInChildren<AudioSource>();
        }

        public void PlaySound(AudioClip clip, bool randomizePitch = true, float minPitch = 0.9f, float maxPitch = 1.1f)
        {
            if (clip == null)
            {
                return;
            }
            if (randomizePitch)
            {
                _audioSource.pitch = Random.Range(minPitch, maxPitch);
            }
            else
            {
                _audioSource.pitch = 1f;
            }
            _audioSource.PlayOneShot(clip);
        }
    }
}
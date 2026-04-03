using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [Header("AudioSource")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _pitchSoundSource;
        [SerializeField] private AudioSource _normalSoundSource;
        [SerializeField] private AudioMixer _audioMixer;

        [Header("AudioClips")]
        [SerializeField] private AudioClip _menuMusic;
        [SerializeField] private AudioClip _gameMusic;
        //sfx
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private AudioClip _deselectSound;
        [SerializeField] private AudioClip _matchSound;
        [SerializeField] private AudioClip _noMatchSound;
        [SerializeField] private AudioClip _whooshSound;
        [SerializeField] private AudioClip _popSound;
        [SerializeField] private AudioClip _stopMusicSound;
        [SerializeField] private AudioClip _removeTileSound;
        [SerializeField] private AudioClip _winSound;
        [SerializeField] private AudioClip _loseSound;

        // gameData

        private bool _isEnabledSound = true;

        public void PlayStopMusic() =>
            PlayNormalPitch(_stopMusicSound);
        public void PlayClick() =>
            PlayNormalPitch(_clickSound);

        public void PlayDeselect() =>
            PlayNormalPitch(_deselectSound);

        public void PlayMatch() =>
            PlayNormalPitch(_matchSound);

        public void PlayNoMatch() =>
            PlayNormalPitch(_noMatchSound);

        public void PlayWin() =>
            PlayNormalPitch(_winSound);

        public void PlayLose() =>
            PlayNormalPitch(_loseSound);

        public void PlayWhoosh() =>
            PlayRandomPitch(_whooshSound);

        public void PlayPop() =>
            PlayRandomPitch(_popSound);

        public void PlayRemove() =>
            PlayRandomPitch(_removeTileSound);

        public void SetSoundVolume()
        {
            if (_isEnabledSound)
            {
                _audioMixer.SetFloat("Volume", -6f);
                _musicSource.Play();
            }
            else
            {
                _audioMixer.SetFloat("Volume", -80f);
                StopMusic();
            }
        }

        public void PlayGameMusic()
        {
            StopMusic();
            _musicSource.clip = _gameMusic;
            SetSoundVolume();
        }

        public void PlayMenuMusic()
        {
            StopMusic();
            _musicSource.clip = _menuMusic;
            SetSoundVolume();
        }

        public void StopMusic() =>
            _musicSource.Stop();

        public void PlayMusic(AudioClip clip)
        {
            StopMusic();
            _musicSource.clip = clip;
            SetSoundVolume();
        }

        private void PlayRandomPitch(AudioClip clip)
        {
            _pitchSoundSource.pitch = Random.Range(0.8f, 1.2f);
            _pitchSoundSource.PlayOneShot(clip);
        }

        private void PlayNormalPitch(AudioClip clip) =>
            _normalSoundSource.PlayOneShot(clip);
    }
}
using UnityEngine;

public class LungeEnemyAudio : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] AudioSource lungeAudioSource;
    [SerializeField] AudioSource pauseAudioSource;

    public void PlayLungeAudio()
    {
        lungeAudioSource.Play();
    }

    public void PlayPauseAudio()
    {
        pauseAudioSource.Play();
    }
}

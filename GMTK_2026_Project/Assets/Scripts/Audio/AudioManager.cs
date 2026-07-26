using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    [Header("Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource playerHealthNotifSource;
    [SerializeField] AudioSource playerLoseGameSource;
    [SerializeField] AudioSource allEnemiesDieSource;

    [Header("Snapshot")]
    [SerializeField] AudioMixerSnapshot normalMusicSnapshot;
    [SerializeField] AudioMixerSnapshot slowedMusicSnapshot;
    public float transitionTime = 0.5f; 

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void PlayPlayerHealthNotifAudio()
    {
        playerHealthNotifSource.Play();
    }


    public void OnLoseGame()
    {
        playerLoseGameSource.Play();
        musicSource.Stop();
    }

    public void PlayAllEnemiesDieAudio()
    {
        allEnemiesDieSource.Play();
    }

    public void SlowTime()
    {
        slowedMusicSnapshot.TransitionTo(transitionTime);
    }

    public void NormalTime()
    {
        normalMusicSnapshot.TransitionTo(transitionTime);
    }
}

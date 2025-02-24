using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource _audioSource;

    [Header("Audio Clips")]
    public AudioClip _backgroundMusic;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _backgroundMusic;
        _audioSource.Play();
    }
}

using UnityEngine;
using UnityEngine.Video;

public class AudioStarter : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartAudio();
    }

    public void StartAudio()
    {
        _audioSource.Play();
    }
}

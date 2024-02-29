using UnityEngine;
using UnityEngine.Video;

public class AudioStarter : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    private void Start()
    {
        ClipStarter.OnAudioStarted += StartAudio;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        ClipStarter.OnAudioStarted -= StartAudio;
    }

    public void StartAudio()
    {
        _audioSource.Play();
    }
}

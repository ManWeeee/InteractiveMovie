using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class ClipHandler : MonoBehaviour
{
    [SerializeField]
    Clip _clip;
    [SerializeField]
    VideoPlayer _videoPlayer;
    [SerializeField]
    AudioSource _audioSource;

    public static event Action OnClipReady;

    [Inject]
    private void Construct(Clip clip)
    {
       LoadClip(clip);
    }

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponent<AudioSource>();
        _videoPlayer.prepareCompleted += StartClip;
        DecisionMaker.OnDecisionMade += LoadNextClip;
    }

    private void Start()
    {
        SetVideoClip(_clip.VideoClip);
        SetAudioClip(_clip.AudioClip);
        _videoPlayer.Prepare();
    }

    private void OnDestroy()
    {
        DecisionMaker.OnDecisionMade -= LoadNextClip;
    }
    private void LoadClip(Clip clip)
    {
        _clip = clip;
        /*_starter.StartVideo();
        _starter.StartSound();*/
    }
    private void LoadNextClip(int index)
    {
        _clip = _clip.GetNextClip(index);
        /*_starter.StartVideo();
        _starter.StartSound();*/
    }

    public void StartClip(VideoPlayer source)
    {
        if (source.isPrepared)
        {
            StartVideo();
        }
        StartAudio();
    }

    public void StartAudio()
    {
        _audioSource.Play();
    }
    public void StartVideo()
    {
        _videoPlayer.Play();
    }
    public void SetVideoClip(VideoClip clip)
    {
        _videoPlayer.clip = clip;
    }
    public void SetAudioClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }
}

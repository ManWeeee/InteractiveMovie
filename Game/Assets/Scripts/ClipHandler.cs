using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Zenject;

public class ClipHandler : MonoBehaviour
{
    [SerializeField]
    GameEvent OnVideoReady;
    [SerializeField]
    Clip _clip;
    [SerializeField]
    VideoPlayer _videoPlayer;
    [SerializeField]
    AudioSource _audioSource;

    public static Action<Clip> OnClipLoaded;

    [Inject]
    private void Construct(Clip clip)
    {
        _clip = clip;
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
        SetVideoClip(_clip.VideoClipName);
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
        SetVideoClip(_clip.VideoClipName);
        SetAudioClip(_clip.AudioClip);
    }
    private void LoadNextClip(int index)
    {
        LoadClip(_clip.GetNextClip(index));
    }

    public void StartClip(VideoPlayer source)
    {
        if (source.isPrepared)
        {
            StartVideo();
            OnVideoReady.Raise();
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
    public void SetVideoClip(string videoClipDataPath)
    {
        _videoPlayer.url = "file://" + Application.streamingAssetsPath + videoClipDataPath;
    }
    public void SetAudioClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }
}

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

    private bool _isFree = true;

    public bool IsFree => _isFree;

    public static Action<Clip> OnClipLoaded;
    public event Action<ClipHandler> OnVideoStarted;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponent<AudioSource>();
        _videoPlayer.prepareCompleted += StartClip;
        if (!_videoPlayer.isLooping)
            _videoPlayer.loopPointReached += (VideoPlayer player) => { _videoPlayer.url = ""; _isFree = true; _videoPlayer.renderMode = VideoRenderMode.CameraFarPlane; };
    }

    public void LoadClip(Clip clip)
    {
        _clip = clip;
        SetVideoClip(clip.VideoClipName);
        SetAudioClip(clip.AudioClip);
    }

    public void StartClip(VideoPlayer source)
    {
        Debug.Log("Clip should Start");
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
        OnVideoStarted?.Invoke(this);
    }
    public void SetVideoClip(string videoClipDataPath)
    {
        _videoPlayer.url = "file://" + Application.streamingAssetsPath + videoClipDataPath;
        _videoPlayer.Prepare();
    }
    public void SetAudioClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }
}

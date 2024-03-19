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

    //public static Action<Clip> OnClipLoaded;
    public Clip Clip => _clip;
    public bool VideoIsPrepared => _videoPlayer.isPrepared;
    public event Action OnVideoStarted;
    public event Action OnVideoFinished;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponent<AudioSource>();
        if (!_videoPlayer.isLooping)
            _videoPlayer.loopPointReached += (VideoPlayer player) => { _videoPlayer.url = ""; OnVideoFinished?.Invoke(); };
    }

    public void LoadClip(Clip clip)
    {
        if (_clip != clip)
        {
            _clip = clip;
            SetVideoClip(clip.VideoClipName);
            SetAudioClip(clip.AudioClip);
        }
    }

    public void StartClip()
    {
        if (VideoIsPrepared)
        {
            Debug.Log("Clip should Start");
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
        OnVideoStarted?.Invoke();
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

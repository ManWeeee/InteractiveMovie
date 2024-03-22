using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Zenject;

public class ClipHandler : MonoBehaviour, IHandler
{
    [SerializeField]
    ClipHandler _nextHandler;
    [SerializeField]
    Clip _clip;
    [SerializeField]
    VideoPlayer _videoPlayer;
    [SerializeField]
    AudioSource _audioSource;

    [SerializeField]
    GameEvent OnVideoReady;
    public Clip Clip => _clip;
    
    public event Action OnVideoPrepared;
    public event Action OnVideoStarted;
    public event Action OnVideoFinished;
    public event Action<ClipHandler> OnDecisionStarted;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponent<AudioSource>();

        if (!_videoPlayer.isLooping)
            _videoPlayer.loopPointReached += (VideoPlayer player) => { _videoPlayer.url = ""; OnVideoFinished?.Invoke(); };
        _nextHandler.OnVideoFinished += StartClip;
    }

    public void Handle(object request)
    {
        if (_videoPlayer.url != "")
        {
            if(_nextHandler != null)
                _nextHandler.Handle(request);
            return;
        }
        if(request as Clip)
            LoadClip(request as Clip);
    }

    public void LoadClip(Clip clip)
    {
        _clip = clip;
        SetVideoClip(clip.VideoClipName);
        SetAudioClip(clip.AudioClip);
    }

    public void StartClip()
    {
        StartVideo();

        StartDecision();

        StartAudio();
    }

    private async void StartDecision()
    {
        await Task.Delay(_clip.DecisionDelaySeconds * 1000);
        OnDecisionStarted?.Invoke(this);
    }

    public void StartAudio()
    {
        _audioSource.Play();
    }

    public async void StartVideo()
    {
        while (!_videoPlayer.isPrepared)
        {
            await Task.Delay(200);
        }
        _videoPlayer.Play();
        OnVideoStarted?.Invoke();
    }

    public async void SetVideoClip(string videoClipDataPath)
    {
        _videoPlayer.url = "file://" + Application.streamingAssetsPath + videoClipDataPath;
        _videoPlayer.Prepare();
        while (!_videoPlayer.isPrepared)
            await Task.Delay(200);
        OnVideoPrepared?.Invoke();
        OnVideoReady.Raise();
    }

    public void SetAudioClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }

}

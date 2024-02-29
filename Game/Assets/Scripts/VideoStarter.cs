using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoStarter : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer _videoPlayer;

    private void Start()
    {
        ClipStarter.OnVideoStarted += StartVideo;
        _videoPlayer = GetComponent<VideoPlayer>(); 
    }

    private void OnDestroy()
    {
        ClipStarter.OnVideoStarted -= StartVideo;
    }

    public void StartVideo()
    {
        _videoPlayer.Play();
    }
}

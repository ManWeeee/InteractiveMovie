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
        _videoPlayer = GetComponent<VideoPlayer>(); 
        StartVideo();
    }

    public void StartVideo()
    {
        _videoPlayer.Play();
    }
}

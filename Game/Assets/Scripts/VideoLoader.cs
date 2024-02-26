using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class VideoLoader : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer _videoPlayer;

    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        ClipLoader.OnClipChanged += LoadVideoClip;
    }

    private void OnDestroy()
    {
        ClipLoader.OnClipChanged -= LoadVideoClip;
    }

    public void LoadVideoClip(Clip clip)
    {
        _videoPlayer.clip = clip.VideoClip;
    }
}

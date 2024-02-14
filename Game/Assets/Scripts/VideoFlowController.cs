using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoFlowController : MonoBehaviour
{
    [SerializeField]
    VideoPlayer _videoPlayer;

    [SerializeField]
    private Clip _clip;

    void Start()
    {
        _videoPlayer.clip = _clip.GetCurrentVideoClip();
    }

    void Update()
    {
        
    }
}

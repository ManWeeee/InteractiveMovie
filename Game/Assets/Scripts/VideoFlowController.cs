using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class VideoFlowController : MonoBehaviour
{
    [SerializeField]
    VideoPlayer _videoPlayer;

    [SerializeField]
    private Clip _clip;

    [Inject]
    private void Construct(VideoPlayer videoPlayer)
    {
        _videoPlayer = videoPlayer;
        _videoPlayer.targetCamera = Camera.main;
    }

    void Start()
    {
        _clip = Resources.Load("MenuClip") as Clip;
        _videoPlayer.clip = _clip.GetCurrentVideoClip();
        _videoPlayer.Play();
    }

    public void LoadNextClip(int sceneIndex)
    {
        _clip = _clip.GetNextClip(sceneIndex);
        _videoPlayer.clip = _clip.GetCurrentVideoClip();
        _videoPlayer.Play();
    }
}

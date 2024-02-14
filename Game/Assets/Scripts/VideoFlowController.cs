using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;


public class VideoFlowController : MonoBehaviour
{
    [SerializeField]
    VideoPlayer _videoPlayer;

    [SerializeField]
    private Clip _clip;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        _clip = Resources.Load("MenuClip") as Clip;
        _videoPlayer.clip = _clip.GetCurrentVideoClip();
        _videoPlayer.Play();
    }

    public void LoadNextClip(int sceneIndex)
    {
        _videoPlayer.clip = _clip.GetNextVideoClip(sceneIndex);
        _videoPlayer.Play();
    }
}

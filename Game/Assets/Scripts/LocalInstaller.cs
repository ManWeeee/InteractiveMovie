using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class LocalInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _videoPlayerPrefab;
    public override void InstallBindings()
    {
        BindVideoPlayer();
    }

    private void BindVideoPlayer()
    {
        VideoPlayer videoPlayerInstance = Container
            .InstantiatePrefabForComponent<VideoPlayer>(_videoPlayerPrefab);

        Container
            .Bind<VideoPlayer>()
            .FromInstance(videoPlayerInstance)
            .AsSingle();
    }
    
}

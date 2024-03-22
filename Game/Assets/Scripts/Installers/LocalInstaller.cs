using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class LocalInstaller : MonoInstaller
{
    /*[SerializeField]
    private GameObject _videoPlayerPrefab;*/
    [SerializeField]
    private Clip _clip;
    public override void InstallBindings()
    {
        BindClip();
    }

/*    private void BindVideoPlayer()
    {
        VideoPlayer videoPlayerInstance = Container
            .InstantiatePrefabForComponent<VideoPlayer>(_videoPlayerPrefab);

        Container
            .Bind<VideoPlayer>()
            .FromInstance(videoPlayerInstance)
            .AsSingle();
    }*/
    

    private void BindClip()
    {
        Container
            .Bind<Clip>()
            .FromInstance(_clip)
            .AsSingle();
    }
}

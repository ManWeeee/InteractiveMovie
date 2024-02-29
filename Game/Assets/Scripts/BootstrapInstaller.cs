using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
/*    [SerializeField]
    GameObject _clipLoader;
    [SerializeField]
    GameObject _clipStarter;*/
    public override void InstallBindings()
    {
        /*BindClipLoader();
        BindClipStarter();*/
    }

/*    private void BindClipLoader()
    {
        ClipLoader clipLoader = Container
            .InstantiatePrefabForComponent<ClipLoader>(_clipLoader);

        Container
            .Bind<ClipLoader>()
            .FromInstance(clipLoader)
            .AsSingle();
    }

    private void BindClipStarter()
    {
        ClipStarter clipStarter = Container
            .InstantiatePrefabForComponent<ClipStarter>(_clipLoader);

        Container
            .Bind<ClipStarter>()
            .FromInstance(clipStarter)
            .AsSingle();
    }*/
}

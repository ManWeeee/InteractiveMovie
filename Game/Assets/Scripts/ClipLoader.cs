using System;
using UnityEngine;
using Zenject;

public class ClipLoader : MonoBehaviour
{
    [SerializeField]
    Clip _clip;
    [SerializeField]
    ClipStarter _starter;

    public static event Action<Clip> OnClipLoaded;

    [Inject]
    private void Construct(Clip clip)
    {
        _clip = clip;
    }
    private void Awake()
    {
        DecisionMaker.OnDecisionMade += LoadNextClip;
    }

    private void OnDestroy()
    {
        DecisionMaker.OnDecisionMade -= LoadNextClip;
    }

    private void Start()
    {
        LoadClip(_clip); 
    }

    private void LoadClip(Clip clip)
    {
        _clip = clip;
        OnClipLoaded?.Invoke(_clip);
        _starter.StartVideo();
        _starter.StartSound();
    }
    private void LoadNextClip(int index)
    {
        _clip = _clip.GetNextClip(index);
        OnClipLoaded?.Invoke(_clip);
        _starter.StartVideo();
        _starter.StartSound();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
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
        _starter = new ClipStarter();
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
        StartCoroutine(_starter.StartVideo(_clip.VideoStartDelay));
        StartCoroutine(_starter.StartSound(_clip.AudioStartDelay));
    }
    private void LoadNextClip(int index)
    {
        _clip = _clip.GetNextClip(index);
        OnClipLoaded?.Invoke(_clip);
        StartCoroutine(_starter.StartVideo(_clip.VideoStartDelay));
        StartCoroutine(_starter.StartSound(_clip.AudioStartDelay));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ClipLoader : MonoBehaviour
{
    [SerializeField]
    Clip _clip;

    public static event Action<Clip> OnClipChanged;
    [Inject]
    private void Construct(Clip clip)
    {
        _clip = clip;
    }

    private void Start()
    {
        LoadClip(_clip);
    }

    public void LoadClip(Clip clip)
    {
        _clip = clip;
        OnClipChanged?.Invoke(_clip);
    }
    private void LoadNextClip(int index)
    {
        _clip = _clip.GetNextClip(index);
        OnClipChanged?.Invoke(_clip);
    }
}

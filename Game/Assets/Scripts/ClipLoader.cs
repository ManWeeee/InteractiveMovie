using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class ClipLoader : MonoBehaviour
{
    [SerializeField]
    ClipHandler clipHandler;
    [SerializeField]
    Clip _clip;

    //public event Action<Clip> OnClipChanged;

    [Inject]
    private void Construct(Clip clip)
    {
        _clip = clip;
    }
    private void Awake()
    {
        DecisionMaker.OnDecisionMade += ChangeNextClipByIndex;
    }
    private void OnDestroy()
    {
        DecisionMaker.OnDecisionMade -= ChangeNextClipByIndex;
    }

    private void Start()
    {
        LoadClip();
        clipHandler.StartClip();
    }

    private void LoadClip()
    {
        clipHandler.Handle(_clip);
        //OnClipChanged?.Invoke(_clip);
    }

    private void ChangeNextClipByIndex(int index)
    {
        Debug.Log(_clip.VideoClipName);
        _clip = _clip.GetNextClip(index);
        LoadClip();
        Debug.Log(_clip.VideoClipName);
    }
}

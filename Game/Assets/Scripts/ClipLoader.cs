using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ClipLoader : MonoBehaviour
{
    [SerializeField]
    VideoSwapper swapper;
    [SerializeField]
    Clip _clip;
    [SerializeField]
    int _nextClipIndex = 0;

    public  VideoSwapper Swapper => swapper;

    [Inject]
    private void Construct(Clip clip)
    {
        _clip = clip;
    }
    private void Awake()
    {
        swapper.OnActiveHandlerSwapped += LoadNextClip;
        DecisionMaker.OnDecisionMade += ChangeNextClipIndex;
    }
    private void OnDestroy()
    {
        swapper.OnActiveHandlerSwapped -= LoadNextClip;
    }

    private async void Start()
    {
        LoadClip(swapper.ActiveHandler);
        while (!swapper.ActiveHandler.VideoIsPrepared)
        {
            await Task.Delay(200);
        }
        swapper.ActiveHandler.StartClip();
    }

    private void LoadClip(ClipHandler handler)
    {
        handler.LoadClip(_clip);
    }

    private void LoadNextClip(ClipHandler handler)
    {
        _clip = _clip.GetNextClip(_nextClipIndex);
        handler.LoadClip(_clip);
        _nextClipIndex = 0;
    }

    private void ChangeNextClipIndex(int index)
    {
        _nextClipIndex = index;
/*        _clip = _clip.GetNextClip(index);
        swapper.InActiveHandler.LoadClip(_clip);*/
    }
}

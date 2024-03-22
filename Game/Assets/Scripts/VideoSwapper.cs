using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class VideoSwapper : MonoBehaviour
{
    [SerializeField]
    ClipHandler[] _clipHandlers;
    [SerializeField]
    ClipHandler _activeHandler;

    public event Action OnActiveHandlerSwapped;

    public ClipHandler ActiveHandler => _activeHandler;

    private void Awake()
    {
        for(int i = 0;  i < _clipHandlers.Length; i++)
        {
            if (_clipHandlers.Length > 1)
            {
                if (i + 1 < _clipHandlers.Length)
                {
                    _clipHandlers[i].OnVideoFinished += _clipHandlers[i + 1].StartClip;
                    _clipHandlers[i + 1].OnVideoFinished += _clipHandlers[i].StartClip;
                }
            } 
        }
        DecisionMaker.OnDecisionDone += SwapActiveHandler;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _clipHandlers.Length; i++)
        {
            if (_clipHandlers.Length > 1)
            {
                if (i + 1 < _clipHandlers.Length)
                {
                    _clipHandlers[i].OnVideoFinished -= _clipHandlers[i + 1].StartClip;
                    _clipHandlers[i + 1].OnVideoFinished -= _clipHandlers[i].StartClip;
                }
            }
        }
        DecisionMaker.OnDecisionDone -= SwapActiveHandler;
    }

    private void Start()
    {
        SwapActiveHandler();
        ActiveHandler.OnVideoPrepared += ActiveHandler.StartClip;
    }

    public void SwapActiveHandler()
    {
        _activeHandler = GetAnotherClipHandler(_activeHandler);
        OnActiveHandlerSwapped?.Invoke();
    }

    private ClipHandler GetAnotherClipHandler(ClipHandler currentActiveHandler)
    {
        ClipHandler tmpHandler = currentActiveHandler;
        foreach (var handler in _clipHandlers)
        {
            if (handler == currentActiveHandler)
                continue;
            tmpHandler = handler;
            break;
        }
        return tmpHandler;
    }
}

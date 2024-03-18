using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class VideoSwaper : MonoBehaviour
{
    /*[SerializeField]
    ClipHandler[] _clipHandlers;
    [SerializeField]
    ClipHandler _activeHandler;

    private void Awake()
    {
        foreach (var clipHandler in _clipHandlers)
        {
            clipHandler.OnVideoFinished += SwapActiveHandler;
        }
    }

    public void SwapActiveHandler(ClipHandler handler)
    {
        _activeHandler = GetAnotherClipHandler(handler);
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
        if (tmpHandler == currentActiveHandler)
            throw new ArgumentException("There is no other ClipHandler");
        return tmpHandler;
    }*/

    [SerializeField]
    ClipHandler[] _clipHandlers;
    [SerializeField]
    ClipHandler _activeHandler;
    [SerializeField]
    Clip _clip;
    [SerializeField]
    float _secondsToMakeAChoice = 5f;

    [Inject]
    private void Construct(Clip clip)
    {
        _clip = clip;
    }
    private void Awake()
    {
        DecisionMaker.OnDecisionMade += LoadNextClip;
    }
    private void Start()
    {
        SwapActiveHandler(_clipHandlers[0]);
        LoadClip();
    }

    private void OnDestroy()
    {
        DecisionMaker.OnDecisionMade -= LoadNextClip;
    }

    public IEnumerator WaitForInput()
    {
        yield return new WaitForSecondsRealtime(_secondsToMakeAChoice);
        LoadNextClip(0);
    }
    private void LoadClip()
    {
        if (_clip.haveChoices)
            _activeHandler.OnVideoStarted += StartDecision;
/*        else
        {
            LoadNextClip(0);
        }*/
        _activeHandler.LoadClip(_clip);
    }

    private void LoadNextClip(int index)
    {
        ClipHandler handler = GetFreeClipHandler();
        Debug.Log($"Handler name{handler.gameObject.name}");
        handler.OnVideoStarted += StartDecision;
        _clip = _clip.GetNextClip(index);
        handler.LoadClip(_clip);
    }

    private void SwapActiveHandler(ClipHandler handler)
    {
        _activeHandler = handler;
    }

    private void StartDecision(ClipHandler handler)
    {
        SwapActiveHandler(handler);
        StartCoroutine(StartDecision());
        _activeHandler.OnVideoStarted -= StartDecision;
    }

    private IEnumerator StartDecision()
    {
        yield return new WaitForSecondsRealtime(_clip.DecisionDelay);
        StartCoroutine(WaitForInput());
    }

    private ClipHandler GetFreeClipHandler()
    {
        foreach (var handler in _clipHandlers)
            if (handler.IsFree)
                return handler;
        return null;
    }
}

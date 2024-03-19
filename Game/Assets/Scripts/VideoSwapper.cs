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

    public event Action<ClipHandler> OnActiveHandlerSwapped;

    public ClipHandler ActiveHandler => _activeHandler;

    public ClipHandler InActiveHandler => GetAnotherClipHandler(_activeHandler);

    private void Awake()
    {
        for(int i = 0;  i < _clipHandlers.Length; i++)
        {
            if (_clipHandlers.Length > 1)
            {
                if (i + 1 < _clipHandlers.Length)
                    _clipHandlers[i].OnVideoFinished += _clipHandlers[i + 1].StartClip;
                else
                    _clipHandlers[i].OnVideoFinished += _clipHandlers[0].StartClip;
            }
            //_clipHandlers[i].OnVideoFinished += SwapActiveHandler;
            DecisionMaker.OnDecisionDone += SwapActiveHandler;
        }
        SwapActiveHandler();
    }


    public void SwapActiveHandler()
    {
        _activeHandler = GetAnotherClipHandler(_activeHandler);
        OnActiveHandlerSwapped?.Invoke(_activeHandler);
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

    /*    [SerializeField]
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
            else
            {
                LoadNextClip(0);
            }
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
        }*/
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DecisionMaker : MonoBehaviour {
    [SerializeField]
    ClipLoader _loader;
    [SerializeField]
    Clip _clip;
    [SerializeField]
    ClipHandler _handler;
    [SerializeField]
    float _secondsToMakeAChoice = 3f;

    public static event Action<int> OnDecisionMade;
    public static event Action OnDecisionDone;

    private void Awake()
    {
        //_loader.Swapper.OnActiveHandlerSwapped += TakeClipHandler;
    }

    public void MakeChoice(int index)
    {
        OnDecisionMade?.Invoke(index);
        OnDecisionDone?.Invoke();
       // Debug.Log("Coroutine Stoped");
       // StopCoroutine(StartDecision());
    }

    private void TakeClipHandler(ClipHandler handler)
    {
        _clip = handler.Clip;
        _handler = handler;
        //_handler.OnVideoStarted += StartDecisionCoroutine;
    }

    private void StartDecisionCoroutine()
    {
        Debug.Log("StartDecisionCoroutine Started");
        StartCoroutine(StartDecisionDelay());
        _handler.OnVideoStarted -= StartDecisionCoroutine;
    }

    private IEnumerator StartDecisionDelay()
    {
        Debug.Log("startDecisionDelay Started");
        yield return new WaitForSeconds(_clip.DecisionDelay);
        Debug.Log("Decision delay passed");
        StartCoroutine(StartDecision());
        
    }

    public IEnumerator StartDecision()
    {
        Debug.Log("wait for Input");
        yield return new WaitForSeconds(_secondsToMakeAChoice);
        Debug.Log("MakeChoice(1)");
        MakeChoice(1);
    }

/*    public void SetActiveButtonsPositions(Clip clip)
    {
        for (int i = 0; i < _choices.Length; i++)
        {
            if (clip.haveChoices)
                SetButtonsPosition(_choices[i], clip.GetPosition(i));
        }
    }

    private void SetButtonsPosition(Button btn, Vector2 position)
    {
        btn.gameObject.GetComponent<Image>().rectTransform.localPosition = position;
    }*/
}

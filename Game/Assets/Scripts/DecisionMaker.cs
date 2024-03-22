using System;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class DecisionMaker : MonoBehaviour 
{
    [SerializeField]
    DecisionMakerUI _decisionMakerUI;
    [SerializeField]
    ClipHandler[] clipHandlers;
    [SerializeField]
    float _secondsToMakeAChoice = 3f;

    public static event Action<int> OnDecisionMade;
    public static event Action OnDecisionDone;
    Coroutine coroutine;
    private void Awake()
    {
        clipHandlers = FindObjectsByType<ClipHandler>(FindObjectsSortMode.InstanceID);
        foreach (var clipHandler in clipHandlers)
            clipHandler.OnDecisionStarted += StartDecisionCoroutine;
    }
    public void MakeChoice(int index)
    {
        OnDecisionMade?.Invoke(index);
        OnDecisionDone?.Invoke();
        StopCoroutine(coroutine);
        _decisionMakerUI.StartHideUICoroutine();
    }

    public void StartDecisionCoroutine(ClipHandler handler)
    {
        coroutine = StartCoroutine(DecisionCoroutine(handler));
    }

    public IEnumerator DecisionCoroutine(ClipHandler handler)
    {
        if (!handler.Clip.haveChoices)
        {
            Debug.Log("Decision(0) was done as it has no inherits");
            MakeChoice(0);
            yield break;
        }
        _decisionMakerUI.StartShowUICoroutine(handler.Clip);
        Debug.Log($"Giving {_secondsToMakeAChoice} seconds for you to make a choice");
        yield return new WaitForSeconds(_secondsToMakeAChoice);
        System.Random rand = new System.Random();
        int index = rand.Next(handler.Clip.NextClips.Length);
        Debug.Log($"MakeChoice({index})");
        MakeChoice(index);
        _decisionMakerUI.StartHideUICoroutine();

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

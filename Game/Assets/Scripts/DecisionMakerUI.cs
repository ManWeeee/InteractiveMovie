using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DecisionMakerUI : MonoBehaviour
{
    [SerializeField]
    Button[] _choices;
    [SerializeField]
    Image _decisionTimeBar;
    [SerializeField]
    float _animationDuration = 0.5f;
    Coroutine _coroutine;
    private void Start()
    {
        HideUI();
    }
    public void StartShowUICoroutine(Clip clip)
    {
        
       _coroutine = StartCoroutine(ShowUICoroutine(clip));
    }

    public void StartHideUICoroutine()
    {
        StartCoroutine(HideUICoroutine());
    }

    private IEnumerator ShowUICoroutine(Clip clip)
    {
        Debug.Log("StartCoroutine");
        ShowUI(clip);
        yield break;
    }

    private IEnumerator HideUICoroutine()
    {
        HideUI();
        yield return null;
    }

    private void ShowUI(Clip clip)
    {
        float alpha = 1f;
        
        int clipLength = clip.NextClips.Length;
        for (int i = 0; i < _choices.Length; i++)
        {
            if (i < clipLength)
            {
                _choices[i].GetComponent<Image>().CrossFadeAlpha(alpha, _animationDuration, true);
                _choices[i].enabled = true;
            }
            else
            {
                _choices[i].GetComponent<Image>().CrossFadeAlpha(0, _animationDuration, true);
                _choices[i].enabled = false;
            }
        }
        _decisionTimeBar.CrossFadeAlpha(alpha, _animationDuration, true);
    }

    private void HideUI()
    {
        float alpha = 0;
        for (int i = 0; i < _choices.Length; i++)
        {
            _choices[i].GetComponent<Image>().CrossFadeAlpha(alpha, _animationDuration, true);

            _choices[i].enabled = false;
        }
        _choices[0].GetComponent<Image>().CrossFadeAlpha(alpha, _animationDuration, false);
        _choices[1].GetComponent<Image>().CrossFadeAlpha(alpha, _animationDuration, false);
        _choices[2].GetComponent<Image>().CrossFadeAlpha(alpha, _animationDuration, false);
        _choices[3].GetComponent<Image>().CrossFadeAlpha(alpha, _animationDuration, false);
        _decisionTimeBar.CrossFadeAlpha(alpha, _animationDuration, true);
    }
}

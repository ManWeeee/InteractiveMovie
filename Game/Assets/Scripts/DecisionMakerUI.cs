using System.Collections;
using System.Collections.Generic;
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

    public void StartShowUICoroutine(Clip clip)
    {
        StartCoroutine(ShowUICoroutine(clip));
    }

    public void StartHideUICoroutine()
    {
        StartCoroutine(HideUICoroutine());
    }

    private IEnumerator ShowUICoroutine(Clip clip)
    {
        ShowUI(clip);
        yield return null;
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
                _choices[i].GetComponent<Image>().CrossFadeAlpha(alpha, _animationDuration, false);
                _choices[i].enabled = true;
            }
            else
            {
                _choices[i].GetComponent<Image>().CrossFadeAlpha(0, _animationDuration, false);
                _choices[i].enabled = false;
            }
        }
        _decisionTimeBar.CrossFadeAlpha(alpha, _animationDuration, false);
    }

    private void HideUI()
    {
        int alpha = 0;

        for (int i = 0; i < _choices.Length; i++)
        {
            _choices[i].GetComponent<Image>().CrossFadeAlpha(alpha, _animationDuration, false);
            _choices[i].enabled = false;
        }

        _decisionTimeBar.CrossFadeAlpha(alpha, _animationDuration, false);
    }
}

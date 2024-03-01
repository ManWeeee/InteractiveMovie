using System;
using UnityEngine;
using UnityEngine.UI;

public class DecisionMaker : MonoBehaviour
{
    [SerializeField]
    Button[] _choices;

    public static event Action<int> OnDecisionMade;

    private void Awake()
    {
        //ClipLoader.OnClipLoaded += SetActiveButtons;
        _choices = new Button[4];
        _choices = GetComponentsInChildren<Button>();
    }

    public void MakeChoice(int index)
    {
        OnDecisionMade?.Invoke(index);
    }

 /*   public void SetActiveButtons(Clip clip)
    {
        for (int i = 0; i < _choices.Length; i++)
        {
            if (clip.haveChoices)
                SetButtonsPosition(_choices[i], clip.GetPosition(i));
        }
    }*/

   /* private void SetButtonState(Button btn, bool state)
    {
        btn.gameObject.SetActive(state);
    }

    private void SetButtonsPosition(Button btn, Vector2 position)
    {
        btn.gameObject.GetComponent<Image>().rectTransform.localPosition = position;
    }*/
}

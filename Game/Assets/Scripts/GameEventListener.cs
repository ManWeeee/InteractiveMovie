using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    GameEvent gameEvent;
    [SerializeField]
    UnityEvent unityEvent;

    private void OnEnable()
    {
        gameEvent.AddListener(this);
    }

    private void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }

    public void Response()
    {
        unityEvent.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new GameEvent", menuName = "Events/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    List<GameEventListener> _listeners = new List<GameEventListener>();

    public void AddListener(GameEventListener listener)
    {
        _listeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        if (_listeners.Contains(listener))
        {
            _listeners.Remove(listener);
        }
    }

    public void Raise()
    {
        for(int i = _listeners.Count - 1; i >= 0; i--) 
        {
            _listeners[i].Response();
        }
    }
}

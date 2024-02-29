using System;
using System.Collections;
using UnityEngine;

public class ClipStarter
{
    public static event Action OnVideoStarted;
    public static event Action OnAudioStarted;
    
    public IEnumerator StartSound(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnAudioStarted?.Invoke();
    }

    public IEnumerator StartVideo(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnVideoStarted?.Invoke();
    }
}

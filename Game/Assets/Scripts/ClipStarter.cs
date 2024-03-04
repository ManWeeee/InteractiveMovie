using System;
using System.Collections;
using UnityEngine;

public class ClipStarter : MonoBehaviour
{
    public static event Action OnVideoStarted;
    public static event Action OnAudioStarted;

    public void StartSound(float seconds)
    {
        StartCoroutine(StartSoundIn(seconds));
    }

    public void StartVideo(float seconds)
    {
        StartCoroutine(StartVideoIn(seconds));
    }

    public IEnumerator StartSoundIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnAudioStarted?.Invoke();
    }

    public IEnumerator StartVideoIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnVideoStarted?.Invoke();
    }
}

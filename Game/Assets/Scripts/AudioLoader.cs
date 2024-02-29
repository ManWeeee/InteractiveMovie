using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class AudioLoader : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSorce;

    void Awake()
    {
        _audioSorce = GetComponent<AudioSource>();
        ClipLoader.OnClipLoaded += LoadAudioClip;
    }

    private void OnDestroy()
    {
        ClipLoader.OnClipLoaded -= LoadAudioClip;
    }

    public void LoadAudioClip(Clip clip)
    {
        _audioSorce.clip = clip.AudioClip;
    }
}

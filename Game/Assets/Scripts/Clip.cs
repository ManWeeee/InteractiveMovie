using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "new Clip", menuName = "Video/Clip", order = 1)]
public class Clip : ScriptableObject
{
    [SerializeField]
    VideoClip _clip;
    [SerializeField]
    Clip[] _nextClips;

    public VideoClip GetCurrentVideoClip()
    {
        return _clip;
    }

    public Clip GetNextClip(int index)
    {
        return _nextClips[index];
    }
}

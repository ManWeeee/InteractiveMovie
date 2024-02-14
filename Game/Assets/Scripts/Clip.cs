using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "new Clip", menuName = "Video/Clip", order = 1)]
public class Clip : ScriptableObject
{
    VideoClip _clip;

    VideoClip[] _nextClips;

    public VideoClip GetCurrentVideoClip()
    {
        return _clip;
    }

    public VideoClip GetNextVideoClip(int index)
    {
        return _nextClips[index];
    }
}

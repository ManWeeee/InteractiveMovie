using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "new Clip", menuName = "Video/Clip", order = 1)]
public class Clip : ScriptableObject
{
    [SerializeField]
    VideoClip _videoClip;
    [SerializeField]
    AudioClip _audioClip;
    [SerializeField]
    Clip[] _nextClips;
    [SerializeField]
    Transform choicePositions;

    public VideoClip VideoClip => _videoClip;
    public AudioClip AudioClip => _audioClip;
    public Clip[] NextClips => _nextClips;

    public Clip GetNextClip(int index)
    {
        return _nextClips[index];
    }
}
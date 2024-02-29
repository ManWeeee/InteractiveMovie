using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "new Clip", menuName = "Video/Clip", order = 1)]
public class Clip : ScriptableObject
{
    [SerializeField]
    VideoClip _videoClip;
    [SerializeField]
    private float _videoStartDealy;
    [SerializeField]
    AudioClip _audioClip;
    [SerializeField]
    private float _audioStartDelay;
    [SerializeField]
    Clip[] _nextClips;
    [SerializeField]
    RectTransform[] choicePositions;

    public VideoClip VideoClip => _videoClip;
    public AudioClip AudioClip => _audioClip;
    public float VideoStartDelay => _videoStartDealy;
    public float AudioStartDelay => _audioStartDelay;
    public Clip[] NextClips => _nextClips;

    public Clip GetNextClip(int index)
    {
        return _nextClips[index];
    }
}
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "new Clip", menuName = "Video/Clip", order = 1)]
public class Clip : ScriptableObject
{
    [SerializeField]
    string _videoClipName;
    [SerializeField]
    AudioClip _audioClip;
    [SerializeField]
    private float _audioStartDelay;
    [SerializeField]
    private int _decisionDelaySeconds;
    [SerializeField]
    Clip[] _nextClips;
    [SerializeField]
    Vector2[] _choicePositions;

    public string VideoClipName { 
        get => _videoClipName; 
        set => _videoClipName = value;
    }
    public AudioClip AudioClip => _audioClip;
    public int DecisionDelaySeconds => _decisionDelaySeconds;
    public float AudioStartDelay => _audioStartDelay;
    public Clip[] NextClips => _nextClips;
    public bool haveChoices => _nextClips.Length > 1;

    public Clip GetNextClip(int index)
    {
        return _nextClips[index];
    }

    public Vector2 GetPosition(int index)
    {
        return _choicePositions[index];
    }
}

/*[CustomPropertyDrawer(typeof(Clip))]
public class VideoDataPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PropertyField(position, property, label, true);

        if (Event.current.type == EventType.DragUpdated || Event.current.type == EventType.DragPerform)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            if (Event.current.type == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();

                foreach (Object obj in DragAndDrop.objectReferences)
                {
                    string path = AssetDatabase.GetAssetPath(obj);
                    if (!string.IsNullOrEmpty(path))
                    {
                        property.stringValue = path;
                    }
                }
            }
        }

        EditorGUI.EndProperty();
    }
}*/
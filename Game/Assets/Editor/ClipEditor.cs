using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Clip))]
public class ClipEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Clip clipData = (Clip)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Select Video"))
        {
            string path = EditorUtility.OpenFilePanel("Select Video File", Application.dataPath, "mp4,avi,mov");
            if (!string.IsNullOrEmpty(path))
            {
                string fileName = Path.GetFileName(path);

                clipData.VideoClipName = "/" + fileName;

                // Mark the object as dirty so changes are saved
                EditorUtility.SetDirty(clipData);
            }
        }
    }
}

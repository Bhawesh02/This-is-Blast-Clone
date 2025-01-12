#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorInspector : Editor
{
    private const string SAVE_DATA_BUTTON_TXT = "Save Data";
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelEditor levelEditor = (LevelEditor)target;
        if (GUILayout.Button(SAVE_DATA_BUTTON_TXT))
        {
            levelEditor.SaveData();
        }
    }
}
#endif

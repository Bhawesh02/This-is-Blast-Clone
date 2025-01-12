#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MyGrid))]
public class GridEditor : Editor
{
    private const string SPAWN_GRID_BUTTON_TEXT = "Spawn MyGrid";
    private const string CLEAR_GRID_BUTTON_TEXT = "Clear MyGrid";
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MyGrid myGrid = (MyGrid)target;
        if (GUILayout.Button(SPAWN_GRID_BUTTON_TEXT))
        {
            myGrid.SpawnGrid();
        }
        if (GUILayout.Button(CLEAR_GRID_BUTTON_TEXT))
        {
            myGrid.ClearGrid();
        }
    }
}
#endif

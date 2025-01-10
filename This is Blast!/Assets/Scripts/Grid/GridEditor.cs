using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    private const string SPAWN_GRID_BUTTON_TEXT = "Spawn Grid";
    private const string CLEAR_GRID_BUTTON_TEXT = "Clear Grid";
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Grid grid = (Grid)target;
        if (GUILayout.Button(SPAWN_GRID_BUTTON_TEXT))
        {
            grid.SpawnGrid();
        }
        if (GUILayout.Button(CLEAR_GRID_BUTTON_TEXT))
        {
            grid.ClearGrid();
        }
    }
}
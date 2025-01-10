using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Data", menuName = "Grid/New Grid Data")]
public class GridData : ScriptableObject
{
    public AxisCombination axisCombination;
    public Vector2 gridSize;
    public float slotSpacing;
    public Slot slotPrefab;
}
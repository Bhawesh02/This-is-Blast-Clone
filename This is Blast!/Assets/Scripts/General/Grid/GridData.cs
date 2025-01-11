﻿using UnityEngine;

[CreateAssetMenu(fileName = "New MyGrid Data", menuName = "Grid/New MyGrid Data")]
public class GridData : ScriptableObject
{
    public AxisCombination axisCombination;
    public Vector2 gridSize;
    public float slotSpacing;
    public Slot slotPrefab;
}
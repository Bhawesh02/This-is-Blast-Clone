using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GridData m_gridData;
    
    private List<Slot> m_spawnedSlots = new();

    public float Columns => m_gridData.gridSize.x;
    public float Rows => m_gridData.gridSize.y;
    
    public void SpawnGrid()
    {
        ClearGrid();
        Vector3 currentPosition = transform.position;
        Vector3 slotSpawnPosition;
        Slot newSlot;
        for (int rowIndex = 0; rowIndex < Rows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < Columns; columnIndex++)
            {
                slotSpawnPosition = currentPosition + GetPostionIncrementBasedOnAxis(rowIndex, columnIndex);
                newSlot = Instantiate(m_gridData.slotPrefab, slotSpawnPosition, Quaternion.identity, transform);
                newSlot.name = $"Slot {rowIndex}, {columnIndex}";
                m_spawnedSlots.Add(newSlot);
            }
        }
    }
    
    private Vector3 GetPostionIncrementBasedOnAxis(int rowIndex, int columnIndex)
    {
        return m_gridData.axisCombination switch
        {
            AxisCombination.XY => new Vector3(rowIndex + (rowIndex * m_gridData.slotSpacing), columnIndex + (columnIndex * m_gridData.slotSpacing), 0f),
            AxisCombination.XZ => new Vector3(rowIndex+ (rowIndex * m_gridData.slotSpacing), 0f, columnIndex + (columnIndex * m_gridData.slotSpacing)),
            AxisCombination.YZ => new Vector3(0f, rowIndex+ (rowIndex * m_gridData.slotSpacing), columnIndex + (columnIndex * m_gridData.slotSpacing))
        };
    }

    public void ClearGrid()
    {
        foreach (Slot slot in m_spawnedSlots)
        {
            DestroySlot(slot);
        }
        m_spawnedSlots.Clear();
    }
    private void DestroySlot(Slot slot)
    {
        if (!slot)
        {
            return;
        }
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
        {
            DestroyImmediate(slot.gameObject);
            return;
        }
#endif
        Destroy(slot.gameObject);
    }
}
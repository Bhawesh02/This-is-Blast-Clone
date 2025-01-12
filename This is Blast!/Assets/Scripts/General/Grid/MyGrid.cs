using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    [SerializeField] private GridData m_gridData;
    
    private List<Slot> m_spawnedSlots = new();

    public int Columns => m_gridData.gridSize.x;
    public int Rows => m_gridData.gridSize.y;
    public List<Slot> Slots => m_spawnedSlots;

    public void SetGridData(GridData data)
    {
        m_gridData = data;
    }
    
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
                newSlot.Config(new(rowIndex, columnIndex), this);
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
            AxisCombination.YZ => new Vector3(0f, rowIndex+ (rowIndex * m_gridData.slotSpacing), columnIndex + (columnIndex * m_gridData.slotSpacing)),
            _ => Vector3.zero
        };
    }

    public void ClearGrid()
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
        {
            while (transform.childCount > 0)
            {
               DestroyImmediate(transform.GetChild(0).gameObject);
            }
            return;
        }
#endif
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


    public Slot GetSlot(Vector2Int coord)
    {
        foreach (Slot spawnedSlot in m_spawnedSlots)
        {
            if (spawnedSlot.Coord == coord)
            {
                return spawnedSlot;
            }
        }
        return null;
    }
}
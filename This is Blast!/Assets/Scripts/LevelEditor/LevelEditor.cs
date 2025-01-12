using System;
using UnityEditor;
using UnityEngine;

public class LevelEditor : MonoSingleton<LevelEditor>
{
    [SerializeField] private GridData m_levelEditorGridData;
    [SerializeField] private MyGrid m_brickGrid;
    [SerializeField] private LevelData m_levelData;
    [SerializeField] private BrickColors m_brickColorToApply;
    
    [SerializeField]
    [Range(1,10)]
    private int m_brickStrength;

    public BrickColors brickColorToApply => m_brickColorToApply;
    public int brickStrength => m_brickStrength;
    
    protected override void Init()
    {
        m_brickGrid.SetGridData(m_levelEditorGridData);
    }

    private void Start()
    {
        SpawnLevel();
    }
    
    private void SpawnLevel()
    {
        ClearLevel();
        m_brickGrid.SpawnGrid();
        foreach (BrickConfigData brickConfigData in m_levelData.brickConfigDatas)
        {
            LevelEditorSlot slot = (LevelEditorSlot)m_brickGrid.GetSlot(brickConfigData.slotCoord);
            slot.InitBrickData(brickConfigData);
        }
    }

    private void ClearLevel()
    {
        m_brickGrid.ClearGrid();
    }
#if UNITY_EDITOR
    public void SaveData()
    {
        m_levelData.brickConfigDatas.Clear();
        foreach (Slot slot in m_brickGrid.Slots)
        {
            LevelEditorSlot levelEditorSlot = (LevelEditorSlot)slot;
            m_levelData.brickConfigDatas.Add(levelEditorSlot.CurrentBrickConfig);
        }
        AssetDatabase.SaveAssets();
    }
#endif
    
}
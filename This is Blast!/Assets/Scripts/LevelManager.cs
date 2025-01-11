using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private MyGrid m_brickGrid;
    [SerializeField] private MyGrid m_shooterGrid;
    [SerializeField] private LevelData m_forceLevelToSpawn;
    
    protected override void Init()
    {
        SpawnLevel();
    }

    private void SpawnLevel()
    {
        ClearLevel();
        m_brickGrid.SpawnGrid();
        foreach (BrickConfigData brickConfigData in m_forceLevelToSpawn.brickConfigDatas)
        {
            BrickSpawnSlot slot = (BrickSpawnSlot)m_brickGrid.GetSlot(brickConfigData.slotCoord);
            slot.InitBrickData(brickConfigData);
        }
    }

    private void ClearLevel()
    {
        m_brickGrid.ClearGrid();
    }
}
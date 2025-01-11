using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private MyGrid m_brickGrid;
    [SerializeField] private MyGrid m_shooterGrid;
    [SerializeField] private LevelData m_forceLevelToSpawn;

    private LevelData m_currentLevelData;
    
    protected override void Init()
    {
        if (m_forceLevelToSpawn)
        {
            m_currentLevelData = m_forceLevelToSpawn;
        }
        SpawnLevel();
    }

    private void SpawnLevel()
    {
        ClearLevel();
        SpawnBricks();
        SpawnShooters();
    }

    private void SpawnBricks()
    {
        m_brickGrid.SpawnGrid();
        foreach (BrickConfigData brickConfigData in m_currentLevelData.brickConfigDatas)
        {
            BrickSpawnSlot slot = (BrickSpawnSlot)m_brickGrid.GetSlot(brickConfigData.slotCoord);
            slot.InitBrickData(brickConfigData);
        }
    }
    
    private void SpawnShooters()
    {
        m_shooterGrid.SpawnGrid();
        ShooterGameSlot shooterSlot;
        Vector2 slotCoord;
        ShooterConfigData shooterConfigData;
        int columnCount = m_shooterGrid.Columns - 1;
        int shooterDataColumn;
        for (int columnIndex = columnCount; columnIndex >= 0; columnIndex--)
        {
            for (int rowIndex = 0; rowIndex < m_shooterGrid.Rows; rowIndex++)
            {
                shooterDataColumn = columnCount - columnIndex;
                if (shooterDataColumn >= m_currentLevelData.shooterSpawnRowDatas.Count)
                {
                    break;
                }
                shooterConfigData = m_currentLevelData.shooterSpawnRowDatas[shooterDataColumn]
                    .shooterRowData[rowIndex];
                slotCoord = new(rowIndex, columnIndex);
                shooterSlot = (ShooterGameSlot)m_shooterGrid.GetSlot(slotCoord);
                if (!shooterSlot)
                {
                    break;
                }
                shooterSlot.SpawnShooter(shooterConfigData);
            }
        }
    }
    
    private void ClearLevel()
    {
        m_brickGrid.ClearGrid();
        m_shooterGrid.ClearGrid();
    }
}
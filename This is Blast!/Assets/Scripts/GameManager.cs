using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private ShooterShootingSlots m_shooterShootingSlots;
    [SerializeField] private MyGrid m_brickSpawnGrid;
    [SerializeField] private Transform m_noProjectileLeftShooterHolder;

    private int m_numberOfBricksInLevel;
    
    public Vector3 NoProjectileLeftShooterHolder => m_noProjectileLeftShooterHolder.position;
    
    protected override void Init()
    {
        GameplayEvents.OnLevelSpawned += HandleOnLevelSpawned;
        GameplayEvents.OnBrickDestroyed += HandleOnBrickDestroyed;
    }

    private void OnDestroy()
    {        
        GameplayEvents.OnLevelSpawned -= HandleOnLevelSpawned;
        GameplayEvents.OnBrickDestroyed -= HandleOnBrickDestroyed;
    }
    
    private void HandleOnLevelSpawned(LevelData levelData)
    {
        m_numberOfBricksInLevel = levelData.GetTotalNumberOfBricks();
    }

    private void HandleOnBrickDestroyed()
    {
        m_numberOfBricksInLevel--;
        if (m_numberOfBricksInLevel == 0)
        {
            GameplayEvents.SendOnLevelCompleted();
        }
    }
    
    public Transform GetNextShooterShootingSlot(Shooter shooter)
    {
        return m_shooterShootingSlots.GetNextFreeShootingSlot(shooter);
    }

    public List<Brick> GetBricksToShootAt()
    {
        List<Brick> bricksToShootAt = new List<Brick>();
        int rowIndex = 0;
        Slot slot;
        Vector2Int slotCoord = Vector2Int.zero;
        for (int columnIndex = 0; columnIndex < m_brickSpawnGrid.Columns; columnIndex++)
        {
            slotCoord.y = rowIndex;
            slotCoord.x = columnIndex;
            slot = m_brickSpawnGrid.GetSlot(slotCoord);
            if (slot.IsOccupied)
            {
                bricksToShootAt.Add((Brick)slot.OccupiedElement);
            }
        }

        return bricksToShootAt;
    }
}
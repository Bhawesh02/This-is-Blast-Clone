﻿using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private ShooterShootingSlots m_shooterShootingSlots;
    [SerializeField] private MyGrid m_brickSpawnGrid;
    
    protected override void Init()
    {
        //NOthing
    }

    public Transform GetNextShooterShootingSlot()
    {
        return m_shooterShootingSlots.GetNextFreeShootingSlot();
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
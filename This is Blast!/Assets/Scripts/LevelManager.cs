﻿using System;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private MyGrid m_brickGrid;
    [SerializeField] private MyGrid m_shooterGrid;
    [SerializeField] private LevelData m_forceLevelToSpawn;
    [SerializeField] private ShooterShootingSlots m_shooterShootingSlots;
    
    private LevelData m_currentLevelData;
    private int m_currentLevelIndex = 0;
    private bool m_isLevelPassed;
    
    protected override void Init()
    {
        GameplayEvents.OnLevelCompleted += HandleOnLevelCompleted;
        GameplayEvents.OnLevelFailed += HandleOnLevelFailed;
        GameplayEvents.OnLevelUIShown += HandleOnLevelUIShown;
    }


    private void OnDestroy()
    {
        GameplayEvents.OnLevelCompleted -= HandleOnLevelCompleted;
        GameplayEvents.OnLevelFailed -= HandleOnLevelFailed;
        GameplayEvents.OnLevelUIShown -= HandleOnLevelUIShown;
    }

    private void HandleOnLevelCompleted()
    {
        m_isLevelPassed = true;
    }

    private void HandleOnLevelFailed()
    {
        m_isLevelPassed = false;
    }
    
    
    private void HandleOnLevelUIShown()
    {
        if (m_isLevelPassed)
        {
            SpawnLevel();
        }
        else
        {
            RestartLevel();
        }
    }
    
    private void Start()
    {
        SpawnLevel();
    }
    
    private void RestartLevel()
    {
        m_currentLevelIndex--;
        SpawnLevel();
    }
    
    private void SpawnLevel()
    {
        ClearLevel();
        GetLevelToSpawn();
        SpawnBricks();
        SpawnShooters();
        m_shooterShootingSlots.SpawnSlots(m_currentLevelData.numberToShootingSlotsToSpawn);
        GameplayEvents.SendOnLevelSpawned(m_currentLevelData);
    }
    
    private void ClearLevel()
    {
        m_brickGrid.ClearGrid();
        m_shooterGrid.ClearGrid();
        m_shooterShootingSlots.ClearSlots();
    }
    
    private void GetLevelToSpawn()
    {
        if (m_forceLevelToSpawn)
        {
            m_currentLevelData = m_forceLevelToSpawn;
            return;
        }
        m_currentLevelData = GameConfig.Instance.levelDatas[m_currentLevelIndex++];
        if (m_currentLevelIndex >= GameConfig.Instance.levelDatas.Count)
        {
            m_currentLevelIndex = 0;
        }
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
        Vector2Int shooterGridSize = m_shooterGrid.GridSize;
        shooterGridSize.y = m_currentLevelData.shooterSpawnRowDatas.Count;
        m_shooterGrid.OverrideGridSize(shooterGridSize);
        m_shooterGrid.SpawnGrid();
        ShooterGameSlot shooterSlot;
        Vector2Int slotCoord;
        ShooterConfigData shooterConfigData;
        int shooterSpawnRowIndex;
        for (int rowIndex = m_shooterGrid.Rows - 1; rowIndex >= 0 ; rowIndex--)
        {
            for (int columnIndex = 0; columnIndex < m_shooterGrid.Columns; columnIndex++)
            {
                shooterSpawnRowIndex = m_shooterGrid.Rows - rowIndex - 1;
                shooterConfigData = m_currentLevelData.shooterSpawnRowDatas[shooterSpawnRowIndex]
                    .shooterRowData[columnIndex];
                slotCoord = new(columnIndex, rowIndex);
                shooterSlot = (ShooterGameSlot)m_shooterGrid.GetSlot(slotCoord);
                if (!shooterSlot)
                {
                    break;
                }
                shooterSlot.SpawnShooter(shooterConfigData);
            }
        }

        if (m_shooterGrid.Rows > 3)
        {
            Vector3 newPosition = m_shooterGrid.transform.position;
            newPosition.z -= m_shooterGrid.Rows - 3;
            m_shooterGrid.transform.position = newPosition;
        }
    }
    
    
}
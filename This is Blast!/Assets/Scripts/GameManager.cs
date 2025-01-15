using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private ShooterShootingSlots m_shooterShootingSlots;
    [SerializeField] private MyGrid m_brickSpawnGrid;
    [SerializeField] private Transform m_noProjectileLeftShooterHolder;

    private int m_numberOfBricksInLevel;
    private bool m_isLevelFailed;
    
    public Vector3 NoProjectileLeftShooterHolder => m_noProjectileLeftShooterHolder.position;
    
    protected override void Init()
    {
        SetupGame();
        GameplayEvents.OnLevelSpawned += HandleOnLevelSpawned;
        GameplayEvents.OnBrickDestroyed += HandleOnBrickDestroyed;
        GameplayEvents.OnNoShooterHasBrickToShootAt += CheckForLevelFail;
    }

    private void SetupGame()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        Vector2 refrenceResolution = GameConfig.Instance.refrenceResolution;
        float referenceAspectRatio = refrenceResolution.x / refrenceResolution.y;
        m_mainCamera.orthographicSize *= referenceAspectRatio / m_mainCamera.aspect;
    }

    private void OnDestroy()
    {        
        GameplayEvents.OnLevelSpawned -= HandleOnLevelSpawned;
        GameplayEvents.OnBrickDestroyed -= HandleOnBrickDestroyed;
        GameplayEvents.OnNoShooterHasBrickToShootAt -= CheckForLevelFail;
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
            Debug.Log("Level Completed");
            GameplayEvents.SendOnLevelCompleted();
        }
    }
    
    private void CheckForLevelFail()
    {
        if (m_numberOfBricksInLevel == 0 || m_isLevelFailed)
        {
            return;
        }
        m_isLevelFailed = true;
        Debug.Log("Level Failed");
        GameplayEvents.SendOnOnLevelFailed();
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
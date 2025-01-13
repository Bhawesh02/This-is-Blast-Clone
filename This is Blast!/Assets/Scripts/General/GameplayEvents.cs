using System;
using Unity.VisualScripting;
using UnityEngine;

public static class GameplayEvents
{
    public static event Action<LevelData> OnLevelSpawned;

    public static void SendOnLevelSpawned(LevelData currentLevelData)
    {
        OnLevelSpawned?.Invoke(currentLevelData);
    }
    
    public static event Action OnBrickDestroyed;

    public static void SendOnBrickDestroyed()
    {
        OnBrickDestroyed?.Invoke();
    }
    public static event Action<Vector2Int> OnBrickSlotEmpty;

    public static void SendOnBrickSlotEmpty(Vector2Int slotCoord)
    {
        OnBrickSlotEmpty?.Invoke(slotCoord);
    } 
    public static event Action<Vector2Int> OnShooterSlotEmpty;

    public static void SendOnShooterSlotEmpty(Vector2Int slotCoord)
    {
        OnShooterSlotEmpty?.Invoke(slotCoord);
    }
    
    public static event Action<Shooter, bool> OnShooterFindingBrickToShotAt;
    
    public static void SendOnShooterFindingBrickToShotAt(Shooter shooter, bool brickFound)
    {
        OnShooterFindingBrickToShotAt?.Invoke(shooter,brickFound);
    }

    public static event Action OnNoShooterHasBrickToShootAt;

    public static void SendOnNoShooterHasBrickToShootAt()
    {
        OnNoShooterHasBrickToShootAt?.Invoke();
    }
    
    public static event Action<Shooter> OnShooterShootingStateExit;

    public static void SendOnShooterShootingStateExit(Shooter shooter)
    {
        OnShooterShootingStateExit?.Invoke(shooter);
    }

    
    
    public static event Action OnLevelCompleted;

    public static void SendOnLevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
}
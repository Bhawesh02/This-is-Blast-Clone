using System;
using Unity.VisualScripting;
using UnityEngine;

public static class GameplayEvents
{
    public static event Action<Brick> OnBrickDestroyed;

    public static void SendOnBrickDestroyed(Brick destroyedBrick)
    {
        OnBrickDestroyed?.Invoke(destroyedBrick);
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

    public static event Action<Shooter> OnShooterShootingStateExit;

    public static void SendOnShooterShootingStateExit(Shooter shooter)
    {
        OnShooterShootingStateExit?.Invoke(shooter);
    }
}
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
    public static event Action<Vector2> OnBrickSlotEmpty;

    public static void SendOnBrickSlotEmpty(Vector2 slotCoord)
    {
        OnBrickSlotEmpty?.Invoke(slotCoord);
    }
}
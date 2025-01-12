using System;

#if UNITY_ANDROID

public class HapticManager : MonoSingleton<HapticManager>
{
    private HapticAndroid m_hapticAndroid;
    protected override void Init()
    {
        m_hapticAndroid = new();
        GameplayEvents.OnBrickDestroyed += HandleOnBrickDestroyed;
    }

    private void OnDestroy()
    {
        GameplayEvents.OnBrickDestroyed -= HandleOnBrickDestroyed;
    }

    private void HandleOnBrickDestroyed()
    {
        m_hapticAndroid.CallHaptic(GameConfig.Instance.brickDestroyedHaptic);
    }
}
#endif


using UnityEngine;

#if UNITY_ANDROID
public class HapticAndroid
{
    private AndroidJavaClass m_hapticFeedbackConstant;
    private AndroidJavaObject m_unityPlayer;

    public HapticAndroid()
    {
#if !UNITY_EDITOR
        m_hapticFeedbackConstant = new AndroidJavaClass("android.view.HapticFeedbackConstants");
        m_unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
            .GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
#endif

    }
    public void CallHaptic(HapticType hapticType)
    {
        string hapticTypeString = $"{hapticType}";
        Debug.Log("Andorid Haptic Trigger : "+hapticTypeString);
#if !UNITY_EDITOR
        int hapticFeedbackConstantKey = m_hapticFeedbackConstant.GetStatic<int>(hapticTypeString);
        m_unityPlayer.Call<bool>("performHapticFeedback", hapticFeedbackConstantKey, 0);
#endif
    }
}
#endif
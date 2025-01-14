using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    private const string LEVEL_COMPLETED_TEXT = "Victory";
    private const string LEVEL_FAILED_TEXT = "Victory";
    private const float RIBBION_SCALE_UP_DURATIOn = 0.5f;
    
    [SerializeField] private GameObject m_levelCompletedScreen;
    [SerializeField] private GameObject m_ribbion;
    [SerializeField] private TextMeshProUGUI m_ribbionTextMesh;
    
    private void Awake()
    {
        GameplayEvents.OnLevelFailed += HandleOnLevelFailed;
        GameplayEvents.OnLevelCompleted += HandleOnLevelCompleted;
    }

    private void OnDestroy()
    {
        GameplayEvents.OnLevelFailed -= HandleOnLevelFailed;
        GameplayEvents.OnLevelCompleted -= HandleOnLevelCompleted;
    }

    private void HandleOnLevelCompleted()
    {
        ShowLevelCompletedScreen(LEVEL_COMPLETED_TEXT);
    }

    private void HandleOnLevelFailed()
    {
        ShowLevelCompletedScreen(LEVEL_FAILED_TEXT);
    }

    private void ShowLevelCompletedScreen(string text)
    {
        m_ribbionTextMesh.text = text;
        Vector3 ribbionScale = m_ribbion.transform.localScale;
        m_ribbion.transform.localScale = Vector3.zero;
        m_levelCompletedScreen.SetActive(true);
        m_ribbion.transform.DOScale(ribbionScale, RIBBION_SCALE_UP_DURATIOn);
        StartCoroutine(CoroutineUtils.Delay(GameConfig.Instance.levelCompletedScreenShowDelay, () =>
        {
            GameplayEvents.SendOnLevelUIShown();
            m_levelCompletedScreen.SetActive(false);
        }));
    }
}
using UnityEngine;

public class LevelEditorSlot : BrickSpawnSlot
{
    
    public override void HandleClick()
    {
        UpdateCurrentBrickConfig();
    }
    
    public override void HandleDrag()
    {
        UpdateCurrentBrickConfig();
    }

    private void UpdateCurrentBrickConfig()
    {
        BrickColors brickColor = LevelEditor.Instance.brickColorToApply;
        int brickStrength = LevelEditor.Instance.brickStrength;
        m_currentBrickConfigData.brickColor = brickColor;
        m_currentBrickConfigData.brickStrenght = brickStrength;
        SpawnBrickOntoSlot();
    }
    
    
}
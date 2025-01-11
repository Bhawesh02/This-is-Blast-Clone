using UnityEngine;

public class Shooter : SlotElement
{
    [SerializeField]
    private MeshRenderer m_modelMeshRenderer;

    private ShooterConfigData m_shooterConfigData;
    
    public void Config(ShooterConfigData shooterConfigData, ShooterElementData shooterElementData, Slot slot, MyGrid grid)
    {
        Config(shooterElementData, slot, grid);
        m_shooterConfigData = shooterConfigData;
        transform.localPosition = shooterElementData.positionOnSlot;
        m_modelMeshRenderer.material = GameConfig.Instance.GetBrickMaterial(m_shooterConfigData.brickColor);
    }
    
    public override void HandleClick()
    {
        //TODO
    }

    public override void HandleDrag()
    {
        //TODO
    }
}
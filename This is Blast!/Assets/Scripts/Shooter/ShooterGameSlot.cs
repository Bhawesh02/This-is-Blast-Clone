using System;
using UnityEngine;

public class ShooterGameSlot : Slot
{
    private ShooterElementData m_shooterElementData;

    private void Awake()
    {
        m_shooterElementData = GameConfig.Instance.shooterElementData;
    }

    public void SpawnShooter(ShooterConfigData shooterConfigData)
    {
        Shooter shooter = (Shooter)Instantiate(m_shooterElementData.elementPrefab, transform);
        shooter.Config(shooterConfigData, m_shooterElementData, this, m_grid);
    }

    public override void EmptySlot()
    {
        base.EmptySlot();
        GameplayEvents.SendOnShooterSlotEmpty(Coord);
    }

    public override void HandleClick()
    {
        //Nothing
    }

    public override void HandleDrag()
    {
        //Nothing
    }
}
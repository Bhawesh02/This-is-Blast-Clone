using System.Collections.Generic;
using UnityEngine;

public class ShooterShootingState : ShooterState
{
    private List<Brick> m_bricksToShootAt = new();
    public ShooterShootingState(Shooter shooter, ShooterStates shooterState) : base(shooter, shooterState)
    {
    }

    public override void OnEnter()
    {
        //TODO
    }

    public override void OnExit()
    {
        //TODO
    }

    public override void OnUpdate()
    {
        //TODO
        m_bricksToShootAt.Clear();
        foreach (Brick brick in GameManager.Instance.GetBricksToShootAt())
        {
            if (brick.BrickConfigData.brickColor == m_shooter.ShooterColor)
            {
                m_bricksToShootAt.Add(brick);
            }
        }
        
    }

    public override void OnClick()
    {
        //TODO
    }
}
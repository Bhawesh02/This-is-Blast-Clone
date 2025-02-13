﻿using DG.Tweening;
using UnityEngine;

public class ShooterWaitingState : ShooterState
{
    private const float INCREMENT_Y_DURATION = 0.1f;
    
    public ShooterWaitingState(Shooter shooter, ShooterStates shooterState) : base(shooter, shooterState)
    {
    }

    public override void OnEnter()
    {
        Transform shooterTransform = m_shooter.transform;
        float shooterNewY = shooterTransform.localPosition.y + GameConfig.Instance.shooterWaitingStateYIncrement;
        shooterTransform.DOLocalMoveY(shooterNewY, INCREMENT_Y_DURATION);
    }

    public override void OnExit()
    {
        //Nothing 
    }

    public override void OnUpdate()
    {
        //Nothing 
    }

    public override void OnDestroyed()
    {
        //Norhing
    }
    
    public override void OnClick()
    {
        Transform shootingSlotTransform = GameManager.Instance.GetNextShooterShootingSlot(m_shooter);
        if (!shootingSlotTransform)
        {
            return;
        }
        Vector3 newPosition = shootingSlotTransform.position + m_shooter.transform.localPosition;
        m_shooter.EmptySlot();
        m_shooter.transform.SetParent(shootingSlotTransform);
        m_shooter.SetCurrentShootingSlot(shootingSlotTransform);
        m_shooter.MoveToPosition(newPosition, ShooterStates.SHOOTING);
    }
}
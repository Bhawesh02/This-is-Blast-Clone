using DG.Tweening;
using UnityEngine;

public class ShooterWalkingState : ShooterState
{
    private float ROATION_RESET_DURATION = 0.05f;
    
    private Vector3 m_newPosition;
    private ShooterStates m_shooterStateToChangeToAfterReaching;
    
    public ShooterWalkingState(Shooter shooter, ShooterStates shooterState) : base(shooter, shooterState)
    {
    }

    public void SetNewPositionAndState(Vector3 newPosition, ShooterStates shooterState)
    {
        m_newPosition = newPosition;
        m_shooterStateToChangeToAfterReaching = shooterState;
    }
    
    public override void OnEnter()
    {
        m_shooter.transform.DOMove(m_newPosition, GameConfig.Instance.shooterMoveSpeed)
            .SetSpeedBased(true).SetEase(GameConfig.Instance.shooterMoveEase)
            .OnComplete(() =>
            {
                m_shooter.SwitchState(m_shooterStateToChangeToAfterReaching);
            });
    }

    public override void OnExit()
    {
        m_shooter.transform.DORotate(Vector3.zero, ROATION_RESET_DURATION);
    }

    public override void OnUpdate()
    {
        Quaternion rotation = Quaternion.LookRotation(m_newPosition - m_shooter.transform.position, Vector3.up);
        m_shooter.transform.rotation = rotation;
    }

    public override void OnClick()
    {
        //TODO
    }
}
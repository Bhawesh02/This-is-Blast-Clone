using UnityEngine;

public class ShooterIdleState : ShooterState
{
    private Vector2Int m_nextSlotCoord; 
    
    public ShooterIdleState(Shooter shooter, ShooterStates shooterState) : base(shooter, shooterState)
    {
    }

    public override void OnEnter()
    {
        m_nextSlotCoord = m_shooter.OccupiedSlotCoord;
        m_nextSlotCoord.y++;
        GameplayEvents.OnShooterSlotEmpty += HandelOnShooterSlotEmpty;
    }

    public override void OnExit()
    {
        GameplayEvents.OnShooterSlotEmpty -= HandelOnShooterSlotEmpty;
    }

    public override void OnUpdate()
    {
        //TODO
    }

    public override void OnClick()
    {
        //TODO
    }
    
    private void HandelOnShooterSlotEmpty(Vector2Int emptySlotCoord)
    {
        if (emptySlotCoord != m_nextSlotCoord)
        {
            return;
        }
        m_shooter.MoveToSlot(emptySlotCoord);
    }
}
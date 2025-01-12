using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private ShooterShootingSlots m_shooterShootingSlots;
    
    protected override void Init()
    {
        //NOthing
    }

    public Transform GetNextShooterShootingSlot()
    {
        return m_shooterShootingSlots.GetNextFreeShootingSlot();
    }
}
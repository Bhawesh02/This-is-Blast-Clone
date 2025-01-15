public abstract class ShooterState
{
    protected Shooter m_shooter;
    protected ShooterStates m_shooterState;

    protected ShooterState(Shooter shooter, ShooterStates shooterState)
    {
        m_shooter = shooter;
        m_shooterState = shooterState;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
    public abstract void OnClick();

    public abstract void OnDestroyed();
}
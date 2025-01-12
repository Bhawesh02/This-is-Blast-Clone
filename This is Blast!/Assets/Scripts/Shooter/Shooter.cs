using System;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class Shooter : SlotElement
{
    [SerializeField]
    private MeshRenderer m_modelMeshRenderer;
    [SerializeField] 
    private TextMeshProUGUI m_projectileCountText;
    
    private ShooterStates m_currentState;
    private ShooterConfigData m_shooterConfigData;
    private ShooterIdleState m_shooterIdleState;
    private ShooterWaitingState m_shooterWaitingState;
    private ShooterWalkingState m_shooterWalkingState;
    private ShooterShootingState m_shooterShootingState;
    private ShooterState m_currentShooterState;
    private Dictionary<ShooterStates, ShooterState> m_shooterStateMap;

    public Vector2Int OccupiedSlotCoord => m_occupiedSlot.Coord;
    public BrickColors ShooterColor => m_shooterConfigData.brickColor;
    public int ShooterProjectileCount => m_shooterConfigData.projectileCount;
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_shooterIdleState = new ShooterIdleState(this, ShooterStates.IDLE);
        m_shooterWaitingState = new ShooterWaitingState(this, ShooterStates.WAITING);
        m_shooterWalkingState = new ShooterWalkingState(this, ShooterStates.WALKING);
        m_shooterShootingState = new ShooterShootingState(this, ShooterStates.SHOOTING);
        m_shooterStateMap = new ()
        {
            { ShooterStates.IDLE , m_shooterIdleState},
            { ShooterStates.WAITING , m_shooterWaitingState},
            { ShooterStates.WALKING , m_shooterWalkingState},
            { ShooterStates.SHOOTING , m_shooterShootingState},
        };
    }

    public void Config(ShooterConfigData shooterConfigData, ShooterElementData shooterElementData, Slot slot, MyGrid grid)
    {
        Config(shooterElementData, slot, grid);
        m_shooterConfigData = shooterConfigData;
        transform.localPosition = shooterElementData.positionOnSlot;
        m_modelMeshRenderer.material = GameConfig.Instance.GetBrickMaterial(m_shooterConfigData.brickColor);
        SwitchState(slot.Coord.y == grid.Rows - 1 ? ShooterStates.WAITING : ShooterStates.IDLE);
        UpdateProjectileCount(ShooterProjectileCount);
    }

    public void SwitchState(ShooterStates newState)
    {
        if (m_currentShooterState != null && newState == m_currentState)
        {
            return;
        }
        m_currentShooterState?.OnExit();
        ShooterState shooterState = m_shooterStateMap.GetValueOrDefault(newState);
        m_currentShooterState = shooterState;
        m_currentShooterState.OnEnter();
        m_currentState = newState;
    }

    public void MoveToPosition(Vector3 newGlobalPosition, ShooterStates nextState)
    {
        m_shooterWalkingState.SetNewPositionAndState(newGlobalPosition, nextState);
        SwitchState(ShooterStates.WALKING);
    }

    public void MoveToSlot(Vector2Int slotCoord)
    {
        Slot nextSlot = m_grid.GetSlot(slotCoord);
        if (nextSlot.IsOccupied)
        {
            return;
        }
        m_occupiedSlot.EmptySlot();
        nextSlot.OccupySlot(this);
        m_occupiedSlot = nextSlot;
        Vector3 newPosition = nextSlot.transform.position + transform.localPosition;
        transform.SetParent(nextSlot.transform);
        MoveToPosition(newPosition, 
            slotCoord.y == m_grid.Rows - 1 ? ShooterStates.WAITING : ShooterStates.IDLE);
    }
    
    private void Update()
    {
        m_currentShooterState?.OnUpdate();
    }

    public override void HandleClick()
    {
        m_currentShooterState?.OnClick();
    }
    
    public void EmptySlot()
    {
        m_occupiedSlot.EmptySlot();
        m_occupiedSlot = null;
    }

    public void LookAtPoint(Vector3 point)
    {
        point.y = transform.position.y;
        Quaternion rotation = Quaternion.LookRotation(point - transform.position, Vector3.up);
        transform.rotation = rotation;
    }
    
    public void UpdateProjectileCount(int projectileCount)
    {
        m_projectileCountText.text = $"{projectileCount}";
    }
    
    
    
    public override void HandleDrag()
    {
        //Nothing
    }

}
using UnityEngine;

public abstract class Slot : MonoBehaviour, IHandleInput
{
    protected Vector2 m_coord;
    protected SlotElement m_slotElement;
    protected bool m_isOccupied;


    public void Init(Vector2 coord)
    {
        m_coord = coord;
        gameObject.name = $"Slot {coord.x}, {coord.y}";
    }
    
    public abstract void HandleClick();
    public abstract void HandleDrag();
}
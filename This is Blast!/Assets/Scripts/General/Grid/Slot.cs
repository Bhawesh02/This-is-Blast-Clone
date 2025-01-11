using UnityEngine;

public abstract class Slot : MonoBehaviour, IHandleInput
{
    protected Vector2 m_coord;
    protected SlotElement m_slotElement;
    protected bool m_isOccupied;

    public Vector2 Coord => m_coord;
    
    public virtual void Config(Vector2 coord)
    {
        m_coord = coord;
        gameObject.name = $"Slot {coord.x}, {coord.y}";
    }

    public virtual void OccupySlot(SlotElement slotElement)
    {
        if (m_isOccupied)
        {
            return;
        }
        m_slotElement = slotElement;
        m_isOccupied = true;
    }

    public virtual void EmptySlot()
    {
        m_slotElement = null;
        m_isOccupied = false;
    }
    
    public abstract void HandleClick();
    public abstract void HandleDrag();
}
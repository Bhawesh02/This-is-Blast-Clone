using UnityEngine;

public abstract class Slot : MonoBehaviour, IHandleInput
{
    protected Vector2Int m_coord;
    protected SlotElement m_slotElement;
    protected bool m_isOccupied;
    protected MyGrid m_grid;
    
    public Vector2Int Coord => m_coord;
    public bool IsOccupied => m_isOccupied;
    
    public virtual void Config(Vector2Int coord, MyGrid grid)
    {
        m_coord = coord;
        gameObject.name = $"Slot {coord.x}, {coord.y}";
        m_grid = grid;
    }

    public virtual void OccupySlot(SlotElement slotElement)
    {
        if (m_isOccupied)
        {
            return;
        }
        m_slotElement = slotElement;
        m_isOccupied = true;
        slotElement.Config(this);
    }

    public virtual void EmptySlot()
    {
        m_slotElement = null;
        m_isOccupied = false;
    }
    
    public abstract void HandleClick();
    public abstract void HandleDrag();
}
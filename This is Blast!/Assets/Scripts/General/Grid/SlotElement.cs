using UnityEngine;

public abstract class SlotElement : MonoBehaviour, IHandleInput
{
    protected SlotElementData m_elementData;
    protected Slot m_occupiedSlot;
    protected MyGrid m_grid;

    public void Config(SlotElementData elementData, Slot occupiedSlot, MyGrid grid)
    {
        m_elementData = elementData;
        m_occupiedSlot = occupiedSlot;
        m_grid = grid;
    }
    public void Config(Slot occupiedSlot)
    {
        m_occupiedSlot = occupiedSlot;
    }
    
    public abstract void HandleClick();
    public abstract void HandleDrag();
}
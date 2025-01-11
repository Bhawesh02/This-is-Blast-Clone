using UnityEngine;

public abstract class SlotElement : MonoBehaviour, IHandleInput
{
    protected SlotElementData m_elementData;
    protected Slot m_occupiedSlot;
    
    public abstract void HandleClick();
    public abstract void HandleDrag();
}
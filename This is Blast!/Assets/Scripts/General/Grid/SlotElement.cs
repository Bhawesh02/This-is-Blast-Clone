using UnityEngine;

public abstract class SlotElement : MonoBehaviour, IHandleInput
{
    private Slot m_occupiedSlot;
    
    public abstract void HandleClick();
    public abstract void HandleDrag();
}
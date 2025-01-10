using UnityEngine;

public abstract class Slot : MonoBehaviour, IHandleInput
{
    private Vector2 m_coord;
    private bool m_isOccupied;
    private SlotElement m_slotElement;


    public abstract void HandleClick();
    public abstract void HandleDrag();
}
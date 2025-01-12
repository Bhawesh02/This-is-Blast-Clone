using UnityEngine;

public class GameBrickSpawnSlot : BrickSpawnSlot
{
    public override void HandleClick()
    {
        //NoThing
    }

    public override void HandleDrag()
    {
        //NoThing
    }
    
    public override void EmptySlot()
    {
        base.EmptySlot();
        GameplayEvents.SendOnBrickSlotEmpty(Coord);
    }
    
    public override void OccupySlot(SlotElement slotElement)
    {
        base.OccupySlot(slotElement);
        Brick brickElement = (Brick)slotElement;
        m_currentBrickConfigData = brickElement.BrickConfigData;
    }
}
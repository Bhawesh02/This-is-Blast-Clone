using System;
using UnityEngine;

public class LevelEditorSlot : Slot
{

    public override void HandleClick()
    {
        //TODO:
        Debug.Log($"Click On {gameObject.name}");
    }
    
    public override void HandleDrag()
    {
        //TODO:
        Debug.Log($"Drag On {gameObject.name}");
    }
}
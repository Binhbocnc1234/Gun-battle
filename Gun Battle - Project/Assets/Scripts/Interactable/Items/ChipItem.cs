using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipItem : Item
{
    // Start is called before the first frame update
    public override void Interact(Player player){
        base.Interact(player);
        player.gunCom.LevelUp();
        DestroyInteractable();
    }
    
}

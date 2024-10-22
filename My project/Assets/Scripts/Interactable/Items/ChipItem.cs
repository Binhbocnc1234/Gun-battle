using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipItem : Item
{
    // Start is called before the first frame update
    public override void Interact(Player player){
        base.Interact(player);
        player.gunCom.LevelUp();
        Destroy(this.gameObject);
    }
    public override void TriggerEnter(Player player){
        base.TriggerEnter(player);
    }
    public override void TriggerExit(Player player){
        base.TriggerExit(player);
    }
    
}

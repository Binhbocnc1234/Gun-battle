using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorItem : Item
{
    public int amount;
    // Start is called before the first frame update
    public override void Interact(Player player){
        audioManager.Play("PickUp");
        player.armor = amount;
        Destroy(this.gameObject);
    }
}

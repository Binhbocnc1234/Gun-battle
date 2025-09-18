using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorItem : Item
{
    public int ammount;
    // Start is called before the first frame update
    public override void Interact(Player player){
        audioManager.Play("PickUp");
        player.GetComponent<Entity>().armor = ammount;
        Destroy(this.gameObject);
    }
}

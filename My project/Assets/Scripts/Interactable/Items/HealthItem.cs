using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    public int amount;
    public override void Interact(Player player){
        audioManager.Play("PickUp");
        player.GetHealth(amount);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    public int amount;
    public override void Trigger(Player player){
        player.GetHealth(amount);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : Item{
    
    public override void Trigger(Player pl){ 
        foreach(Transform child in ObjectHolder.instance.itemList){
            if (child.GetComponent<Item>().itemName == pl.gunCom.gunName){
                var gunItemObj = Instantiate(child, transform.position, transform.rotation);
                gunItemObj.gameObject.SetActive(true);
            }
            
        }
        pl.SwitchGun(itemName);
        Destroy(this.gameObject);
    }
}

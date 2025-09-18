using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPickUp: MonoBehaviour{
    // public void PickUp(Item item){
    //     GunItem gunItem = item.GetComponent<GunItem>();
    //     HealthItem healthItem = item.GetComponent<HealthItem>();
    //     Chest chest = item.GetComponent<Chest>();
    //     if (gunItem != null){
    //         foreach(Transform child in ObjectHolder.instance.itemList){
    //             if (child.GetComponent<Item>().itemName == gunObj.gunName){
    //                 var gunItemObj = Instantiate(child, transform.position, transform.rotation);
    //                 gunItemObj.gameObject.SetActive(true);
    //             }
    //         }
    //         SwitchGun(item.itemName);Destroy(item.gameObject);
    //     }
    //     else if (healthItem != null){
    //         GetHealth(healthItem.amount);
    //         Destroy(item.gameObject);
    //     }
    //     else if (chest != null){
    //         chest.sprite.GetChild(0).gameObject.SetActive(false);
    //         chest.sprite.GetChild(1).gameObject.SetActive(true);
    //         Item obj = Instantiate(chest.item, chest.transform.position, chest.transform.rotation);
    //         obj.gameObject.SetActive(true);
    //     }
        
    // }
    
}

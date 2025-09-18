using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Item
{
    // Start is called before the first frame update
    [HideInInspector] public Transform sprite;
    [HideInInspector] public Item item;
    protected override void Start(){
        base.Start();
        durationTimer.totalTime = duration;
        sprite = transform.GetChild(0);
    }



    ///<sumary>
    /// The chest is opened and item is spawned. After that, you cannot interact with the chest any more
    ///<sumary>
    public override void Interact(Player pl){
        sprite.GetChild(0).gameObject.SetActive(false);
        sprite.GetChild(1).gameObject.SetActive(true);
        Item obj = Instantiate(item, transform.position, transform.rotation);
        obj.gameObject.SetActive(true);
        this.canInteract = false;
        // pl.nearbyInteractable = null;
    }
    public override void TriggerEnter(){

    }
    public override void TriggerExit(){
        
    }
}

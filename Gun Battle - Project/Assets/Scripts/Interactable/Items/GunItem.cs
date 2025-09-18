using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : Item{
    public static Dictionary<GunType, string[]> nextGun = new Dictionary<GunType, string[]>(){
        {GunType.Rifle , new string[]{"AK47", "M4A1", "SCAR"}},
        {GunType.Sniper, new string[]{"AWM", "Barret"}},
        {GunType.Pistol, new string[]{"Colt 45", "Glock"}},
        {GunType.Launcher, new string[]{"Bazooka"}},
        {GunType.Machine, new string[]{"UMP"}}
    };

    public GunModel gunSO;
    // public string nextGun = "None";
    [HideInInspector] public int level;
    protected override void Start(){
        base.Start();
        itemName = gunSO.gunName;
        GetComponent<SpriteRenderer>().sprite = gunSO.textureInBattleField;
    }
    public override void Interact(Player pl){ 
        base.Interact(pl);
        foreach(Transform child in ObjectHolder.instance.itemList){
            if (child.GetComponent<Item>().itemName == pl.gunCom.gunName){
                var gunItemObj = Instantiate(child, transform.position, transform.rotation);
                gunItemObj.gameObject.SetActive(true);
            }
            
        }
        pl.SwitchGun(itemName);
        Destroy(this.gameObject);
    }
    public virtual string GetName(){
        
        if (level > 0){
            string i = "";
            for (int j = 0; j < level ; ++j){
                i = i + "I";
            }return itemName + " - " + i;
        }
        else{return itemName;}
    }
}

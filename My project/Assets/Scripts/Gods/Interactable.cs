using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Observer;
using TMPro;

public enum InteractableType{normal, gun, health, chest, armor}
public enum GunType{Rifle = 0, Sniper, Shotgun, SubMachince, Machine, Pistol, Launcher, Throwing, Other};
public class Interactable : MonoBehaviour
{
    public InteractableType itemType = InteractableType.normal;
    public string itemName;
    public static bool isTriggering;
    [HideInInspector] public bool canInteract = true;
    [HideInInspector] public  bool autoLoot = false;
    protected virtual void Start()
    {
        canInteract = true;
    }

    protected virtual void Update()
    {

    }
    public virtual void Interact(Player player){
        
    }
    public virtual void TriggerEnter(Player player){

    }
    public virtual void TriggerExit(Player player){
        
    }
}

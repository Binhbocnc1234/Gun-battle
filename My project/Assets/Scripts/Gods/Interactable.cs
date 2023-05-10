using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Observer;
using TMPro;

public enum InteractableType{normal, gun, health, chest, armor}
public enum Rarity{Common, UnCommon, Rare, VeryRare, Epic, Legendary, Mythical}
public class Interactable : MonoBehaviour
{
    public InteractableType itemType = InteractableType.normal;
    public string itemName;
    public static bool isShowedInfo;
    [HideInInspector] public bool canInteract = true;
    [HideInInspector] public  bool autoLoot = false;
    public TextMeshPro itemNameText;
    protected virtual void Start()
    {
        canInteract = true;
        itemNameText.text = itemName;
        itemNameText.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {

    }
    public virtual void Trigger(Player player){
        
    }
}

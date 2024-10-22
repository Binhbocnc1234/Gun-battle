using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Observer;
using TMPro;

public enum Rarity{Common, UnCommon, Rare, VeryRare, Epic, Legendary, Mythical}

/// <summary>
/// Some properties;
/// Has no collider and rigidbody
/// </summary>
public class Item : Interactable
{
    
    public static Dictionary<Rarity, Color> rarityColor = new Dictionary<Rarity, Color>(){
        {Rarity.Common, Color.white},
        {Rarity.UnCommon, Color.green},
        {Rarity.Rare, Color.blue},
        {Rarity.VeryRare, new Color(175, 0, 175)}, // purple
        {Rarity.Legendary , Color.red},
        {Rarity.Mythical, Color.magenta}

    };
    public int appearanceChance = 30;
    public Rarity rarity;
    public float duration = 15; //item's lifetime 
    protected AudioManager audioManager;
    protected Timer durationTimer = new Timer(0);
    //Item's text
    public TextMeshPro itemNameText;

    protected override void Start()
    {
        //Item's text
        durationTimer.totalTime = duration;
        itemNameText.text = itemName;
        itemNameText.gameObject.SetActive(false);
        itemNameText.color = rarityColor[rarity];
        //GetComponent;
        foreach(Transform child in transform){
            if (child.GetComponent<AudioManager>() != null){
                audioManager = child.GetComponent<AudioManager>();
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (duration != 0 && durationTimer.Count()){
            Destroy(this.gameObject);
        }
    }
    public override void Interact(Player player){
        audioManager.Play("PickUp");

    }
    public override void TriggerEnter(Player player){
        itemNameText.gameObject.SetActive(true);
    }
    public override void TriggerExit(Player player){
        itemNameText.gameObject.SetActive(false);
    }
}

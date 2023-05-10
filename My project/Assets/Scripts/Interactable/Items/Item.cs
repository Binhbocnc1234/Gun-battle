using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Observer;
using TMPro;

public class Item : Interactable
{
    public float duration = 15;
    protected AudioManager audioManager;
    protected Timer durationTimer = new Timer(0);
    //Item's infomation
    protected override void Start()
    {
        durationTimer.totalTime = duration;
        itemNameText.text = itemName;
        itemNameText.gameObject.SetActive(false);
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
    public override void Trigger(Player player){
        audioManager.Play("PickUp");
    }
}

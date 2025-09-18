using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Observer;
using TMPro;

public enum eInteractableType { Normal, Gun, Health, Chest, Armor, Portal }

public interface IInteractable
{
    public virtual void Interact(Player player) { }
    public virtual void TriggerEnter() { }
    public virtual void TriggerExit() { }
}

public class Interactable : MonoBehaviour, IInteractable
{
    public static List<Interactable> container = new List<Interactable>(); // Static list to track interactable objects
    public eInteractableType itemType = eInteractableType.Normal;
    public string itemName;
    public float maxDistance = 1.5f;
    public static bool isTriggering;
    [HideInInspector] public bool canInteract = true;
    [HideInInspector] public bool autoLoot = false;

    protected virtual void Start()
    {
        canInteract = true;
        container.Add(this); // Add to list when created
    }
    public virtual void Interact(Player player) { }

    public virtual void TriggerEnter() { }

    public virtual void TriggerExit() { }

    public virtual void DestroyInteractable()
    {
        container.Remove(this);
        Destroy(this.gameObject);
    }
}

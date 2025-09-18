using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum eGroup
{
    Player,
    Enemy,
    None
}
public class Entity : MonoBehaviour
{
    [Header("Entity fields")]
    public bool isAlive = true;
    public string group;
    public string entityName;
    public int mainHealth; [HideInInspector] public int health;
    public int mainArmor; [HideInInspector] public int armor;
    public int virtualShield;
    [HideInInspector] public float immuneRate; //range from 0 to 1

    // [HideInIns
    public Animator animator;
    protected SpriteRenderer spriteRenderer;
    new protected Rigidbody2D rigidbody;
    new protected Collider2D collider2D;
    //Event 
    public event Action OnGetDamage, OnDeath;
    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        health = mainHealth;
    }
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (immuneRate == 1)
        {
            spriteRenderer.material.color = Color.yellow;
        }
        else
        {
            spriteRenderer.material.color = Color.white;
        }
    }
    public virtual void GetDamage(int amount)
    {
        if (isAlive == true)
        {
            OnGetDamage?.Invoke();
            health -= (int)(amount * (600.0f / (600.0f + armor)) * (1 - immuneRate));
        }
        CheckAlive();
    }
    public virtual void GetHealth(int amount)
    {
        health += amount;
        health = Mathf.Min(health, mainHealth);
    }
    protected virtual bool CheckAlive()
    {
        isAlive = health > 0;
        if (isAlive == false)
        {
            health = 0; //Ensure that health is alway >= 0
            OnDeath?.Invoke();
            return false;
        }
        return true;
    }
    public virtual void SwitchAnim(string name = "Idle")
    {
        animator.Play(name);
    }
    protected virtual void Destroy()
    {
        Destroy(this.gameObject);
    }
    public virtual void Reset()
    {
        health = mainHealth;
        armor = 0;
        virtualShield = 0;
        immuneRate = 0;
        isAlive = true;
    }

}

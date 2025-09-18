using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
public enum MovementState
{
    Move, //0: player is pressing moving key: A and D
    Jump, //1: : player is jumping but aready performed double jump
    DoubleJump, //2: player can perform another jump
    Climb,
    None
}
[RequireComponent(typeof(Entity))]
public partial class Player : Entity
{
    [Header("Player fields")]
    public static List<Player> container = new List<Player>();
    //Indentify
    public int index;
    //Reference to other classes
    public int usedGunIndex;
    public ListCom gunContainer; 
    public Gun gunCom;
    public Transform reloadAnim;
    // [HideInInspector] public Interactable nearbyInteractable;
    [HideInInspector] public string usedSkin;
    //Physic and movement;
    public float movingSpeed = 7;
    protected float distToGround;
    [HideInInspector] public Vector2 moveDir;
    public float jumpMagnitude = 10;
    [HideInInspector] public bool isFacingRight = true;
    //Event
    public event Action<int> OnPlayerDeath, OnPlayerLoot;
    protected override void Awake()
    {
        // Debug.Log("player awake");
        base.Awake();
        Player.container.Add(this);
        reloadAnim.gameObject.SetActive(false);
        //Reset value
        distToGround = GetComponent<Collider2D>().bounds.extents.y;
        SwitchGun(0);
    }
    protected override void Start()
    {
        base.Start();
        //Reset value
        if (index == 1) { FaceLeft(); }
        usedGunIndex = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        isMoving = moveDir.magnitude > 0.1f;
        HandlingAnimator();
    }
    public override void GetDamage(int amount) {
        base.GetDamage(amount);
        SwitchAnim("Hurt");
        
    }
    protected override bool CheckAlive()
    {
        if (base.CheckAlive() == false)
        {
            OnPlayerDeath?.Invoke(index);
        }
        return true;
    }
    // public override bool GetDamage(){
    //     SwitchAnim("Hurt");
    //     if (base.GetDamage(amount) && isOnDeath == false){
    //         ControllerPvP.instance.RoundEnd(1 - index);
    //         OnDeath();
    //         return true;
    //     }
    //     return false;
    // }

    void OnTriggerEnter2D(Collider2D other){
        //Collide with Wall
        if (other.transform.CompareTag("Wall")){
            isJumping = false;
        }
    }
    void OnTriggerExit2D(Collider2D other){

    }
    public void Fire(){
        
    }
    public void Jump(){
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        if (!isJumping && isGrounded)
        {
            Debug.Log("Jump for the first time");
            ParticleController.Instance.AddLocation(ParticleLocationType.Jump, this.transform.position);
            isJumping = true;
            rigid.AddForce(new Vector2(0, jumpMagnitude));
        }
        else if (isJumping && canPerformDoubleJump)
        {
            Debug.Log("Jump for second time");
            ParticleController.Instance.AddLocation(ParticleLocationType.Jump, this.transform.position);
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpMagnitude));
            canPerformDoubleJump = false;
        }
    }
    public void Loot(){
        OnPlayerLoot?.Invoke(index);
    } 
    public void FaceRight(){
        transform.localScale = new Vector3(1, 1, 1); // Face right
    }

    public void FaceLeft(){
        transform.localScale = new Vector3(-1, 1, 1); // Face left
    }
    public void SwitchGun(string name){
        gunContainer.SwitchElement(name, tr => tr.GetComponent<Gun>().gunName);
        usedGunIndex = gunContainer.selectedIndex;
        gunCom = gunContainer.selectedObj.GetComponent<Gun>();
        gunCom.owner = this.transform;
    }
    public void SwitchGun(int ind){
        gunContainer.SwitchElement(ind);
        usedGunIndex = ind;
        gunCom = gunContainer.selectedObj.GetComponent<Gun>();
        gunCom.owner = this.transform;
    }
    #if UNITY_EDITOR
    
    #endif 
}

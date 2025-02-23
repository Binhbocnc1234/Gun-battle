using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public enum MovementState{
    Move, //0: player is pressing moving key: A and D
    Jump, //1: : player is jumping but aready performed double jump
    DoubleJump, //2: player can perform another jump
    Climb,
    None
}
public partial class Player : Entity
{
    //Indentify
    public int index;
    //Equipments
    public int usedGunIndex;
    public ListCom gunContainer; 
    public Gun gunCom;
    public Transform reloadAnim;
    [HideInInspector] public Interactable nearbyInteractable;
    [HideInInspector] public string usedSkin;
    //Physic and movement;
    public float movingSpeed = 7;
    protected float distToGround;
    [HideInInspector] public Vector2 moveDir;
    public float jumpMagnitude = 10;
    [HideInInspector] public bool isFacingRight = true;
    protected override void Awake(){
        base.Awake();
        reloadAnim.gameObject.SetActive(false);
        //Reset value
        distToGround = collider2D.bounds.extents.y;
        SwitchGun(0);
    }
    protected override void Start()
    {
        base.Start();
        //Reset value
        if(index == 1){FaceLeft();}
        distToGround = collider2D.bounds.extents.y;
        usedGunIndex = 0;
        SwitchGun(ControllerPvP.instance.defaultGun);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (nearbyInteractable != null){
            nearbyInteractable.TriggerEnter(this);
        }
        isMoving = moveDir.magnitude > 0.1f;
        HandlingAnimator();
    }
    public override bool GetDamage(int amount){
        SwitchAnim("Hurt");
        if (base.GetDamage(amount) && isOnDeath == false){
            ControllerPvP.instance.RoundEnd(1 - index);
            OnDeath();
            return true;
        }
        return false;
    }
    public override void GetHealth(int amount){
        base.GetHealth(amount);
    }
    void OnDeath(){
        isOnDeath = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        gunContainer.gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other){
        Bullet bullet = other.GetComponent<Bullet>();
        Interactable inter = other.GetComponent<Interactable>();
        if (inter != null && inter.canInteract){
            nearbyInteractable = inter;
        }
        else if(bullet != null){
            
        }

        //Collide with Wall
        if (other.transform.CompareTag("Wall")){
            isJumping = false;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        nearbyInteractable =  other.GetComponent<Interactable>();
        if (nearbyInteractable != null){
            nearbyInteractable.TriggerExit(this);
            nearbyInteractable = null;

        }
    }
    public void Fire(){
        
    }
    public void Jump(){
        if (!isJumping && isGrounded){
            isJumping = true;
            rigidbody.AddForce(new Vector2(0, jumpMagnitude));
        }
        else if (isJumping && canPerformDoubleJump){
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(new Vector2(0, jumpMagnitude));
            canPerformDoubleJump = false;
        }
    }
    public void Loot(){
        if (nearbyInteractable == null){
            gunCom.StartReload();
        }
        else if (nearbyInteractable.canInteract){
            nearbyInteractable.Interact(this);
        }
        
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
        gunCom.player = this;
    }
    public void SwitchGun(int ind){
        gunContainer.SwitchElement(ind);
        usedGunIndex = ind;
        gunCom = gunContainer.selectedObj.GetComponent<Gun>();
        gunCom.player = this;
    }
    #if UNITY_EDITOR
    
    #endif 
}

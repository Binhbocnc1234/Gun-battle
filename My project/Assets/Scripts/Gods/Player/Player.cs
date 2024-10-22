using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Observer;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Player : Entity
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
    //State
    [HideInInspector] public bool isOnDeath = false, isOnRevive = false, isMoving;
    private bool _isShooting;
    [HideInInspector] public bool isShooting{
        get{return _isShooting;}
        set{
            _isShooting = value;
            if (value){gunCom.gameObject.SetActive(false);}
            else{gunCom.gameObject.SetActive(true);}
        }
    }
    public bool isJumping;
    // Start is called before the first frame update
    protected override void Awake(){
        //Get references to other objects and components
        base.Awake();
        reloadAnim.gameObject.SetActive(false);
        //Register events
        // this.RegisterListener(EventID.OnFiPlayerLoot, (param) => Loot());
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
        // Portal portalCom = other.GetComponent<Portal>();
        Bullet bullet = other.GetComponent<Bullet>();
        Interactable inter = other.GetComponent<Interactable>();
        if (inter != null && inter.canInteract){
            nearbyInteractable = inter;
        }
        else if(bullet != null){
            
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
        rigidbody.AddForce(new Vector2(0, jumpMagnitude));
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
    public bool isGrounded(){
        Debug.DrawRay(transform.position, transform.position + Vector3.down*distToGround, Color.red, 1, false);
        return Physics2D.Raycast(transform.position, Vector3.down, distToGround);
    }
    #if UNITY_EDITOR
    
    #endif 
}

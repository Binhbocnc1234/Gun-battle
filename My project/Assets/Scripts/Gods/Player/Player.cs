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
    //State
    [HideInInspector] public bool isOnDeath = false, isOnRevive = false;
    //Equipments
    public int usedGunIndex;
    public ListCom gunContainer; [HideInInspector] public Gun gunCom;
    public Transform reloadAnim;
    [HideInInspector] public Interactable nearbyInteractable;
    [HideInInspector] public string usedSkin;
    //Physic and movement;
    public float movingSpeed = 10;
    protected float distToGround;
    [HideInInspector] public Vector2 moveDir;
    public float jumpMagnitude = 10;
    [HideInInspector] public bool isFacingRight = true;
    
    //Input
    public PlayerInput playerInput;
    public InputAction moveAction, shotAction, jumpAction, lootAction;
    public InputActionMap inputActionMap;
    // Start is called before the first frame update
    protected override void Awake(){
        
        //Get references to other objects and components
        base.Awake();
        reloadAnim.gameObject.SetActive(false);
        //Input
        playerInput = new PlayerInput();
        if (index == 1){
            inputActionMap = playerInput.Player_2;
        }
        else if (index == 0){
            inputActionMap = playerInput.Player;
        }
        moveAction = inputActionMap.FindAction("Move", true);
        shotAction = inputActionMap.FindAction("Shot", true);
        jumpAction = inputActionMap.FindAction("Jump", true);
        lootAction = inputActionMap.FindAction("Loot", true);
        inputActionMap.Enable();
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
        if(index == 1){Flip();}
        distToGround = collider2D.bounds.extents.y;
        usedGunIndex = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (gunContainer.selectedIndex == -1){
            SwitchGun(0);
        }
    }
    public new void GetDamage(int amount){
        SwitchAnim("Hurt");
        if (base.GetDamage(amount)){
            ControllerPvP.instance.RoundEnd(1 - index);
        }
    }
    public new void GetHealth(int amount){
        base.GetHealth(amount);
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
        else{
            nearbyInteractable.Trigger(this);
        }
        
    } 
    public void Flip(){
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        if (scale.x== 1){
            scale.x = -1;
        }else{scale.x = 1;}
        
        transform.localScale = scale;
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

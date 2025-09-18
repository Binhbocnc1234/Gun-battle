using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Observer;


/// <summary>
/// A class to handle player input <br/>
/// Phase 1 : Moving <br/>
/// W to jump and double jump, S to interact, A and D to move left and right<br/>
/// Phase 2: Shooting <br/>
/// Change shooting direction: W and S to turn muzzle up and down, A and D to change facing direction <br/>
/// Release bullet : F , hold F to shoot continously <br/>
/// Double tap A or D to exit shooting phase<br/>
/// </summary>
public class PlayerInputHandle : MonoBehaviour
{
    Player pl;
    public PlayerInput playerInput;
    public InputAction moveAction, shotAction, jumpAction, lootAction;
    public InputActionMap inputActionMap;
    GunShooting gunShooting;
    // Timer jumpDelay = new Timer(0.4f);
    Timer holdingTimer = new Timer(0.3f);
    private bool isHoldingShootingKey;
    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f;  // Time window for double tap
    private Animator animator;

    //Events
    public event Action OnLootEvent, OnJumpEvent;
    void Awake()
    {
        pl = GetComponent<Player>();
        animator = GetComponent<Animator>();
        playerInput = new PlayerInput();
        if (pl.index == 1)
        {
            inputActionMap = playerInput.Player_2;
        }
        else if (pl.index == 0)
        {
            inputActionMap = playerInput.Player;
        }
        moveAction = inputActionMap.FindAction("Move", true); //key A and D
        shotAction = inputActionMap.FindAction("Shot", true); // key F
        jumpAction = inputActionMap.FindAction("Jump", true); // key W
        lootAction = inputActionMap.FindAction("Loot", true); // key S
        inputActionMap.Enable();
        EnterMovingPhase();
        
    }
    void Update(){
        if (pl.isOnDeath){return;}
        // jumpDelay.Count(false);
        if (pl.isShooting){
            
        }
        else{
            pl.moveDir = moveAction.ReadValue<Vector2>();
            // Debug.Log(pl.moveDir);
            //Make the player rotate in the direction of movement
            if (pl.moveDir.x > 0){pl.FaceRight();}
            else if (pl.moveDir.x < 0){pl.FaceLeft();}
            //Movement
            if (pl.moveDir.x != 0){
                pl.transform.Translate((new Vector3(1,0,0))*pl.moveDir*pl.movingSpeed*Time.deltaTime);
                
            }
        }
    }
    void Start(){
        gunShooting = pl.gunCom.GetComponent<GunShooting>();
    }
    // InputAction callbacks
    public void OnMove(InputAction.CallbackContext context)
    {
        if (pl.isShooting){
            HandleShootingMovement();
        }
        else{
            animator.Play("Move");
            pl.moveDir = context.ReadValue<Vector2>();
            // Debug.Log(pl.moveDir);
            //Make the player rotate in the direction of movement
            if (pl.moveDir.x > 0){pl.FaceRight();}
            else if (pl.moveDir.x < 0){pl.FaceLeft();}
            //Movement
            if (pl.moveDir.x != 0){
                pl.transform.Translate(new Vector3(1,0,0)*pl.moveDir*pl.movingSpeed*Time.deltaTime);
                
            }
        }
    }
    //Haven't used
    public void OnJump(InputAction.CallbackContext context)
    {
        pl.Jump();
    }

    public void OnLootOrSitDown(InputAction.CallbackContext context)
    {
        if (context.started){
            
            Debug.Log("Player loot");
            pl.Loot();
        }
        else if (context.canceled){

        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!pl.isShooting){
            EnterShootingPhase();
        }
        else{
            if (context.performed){
                //shooting
                pl.gunCom.GetComponent<GunShooting>().Shot();
            }
        }
    }

    private void EnterShootingPhase(){
        pl.isShooting = true;
        moveAction.performed -= OnMove;
        // moveAction.performed += HandleShootingMovement;
        jumpAction.started -= OnJump;
        //Remove lootAction
        lootAction.canceled -= OnLootOrSitDown;
        lootAction.started -= OnLootOrSitDown;
        Debug.Log("Entered Shooting Phase");
    }
    private void EnterMovingPhase()
    {
        pl.isShooting = false;
        shotAction.canceled += OnShoot;
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        // moveAction.performed -= HandleShootingMovement;
        jumpAction.canceled += OnJump;
        //Remove lootAction
        lootAction.canceled += OnLootOrSitDown;
        lootAction.started += OnLootOrSitDown;
        Debug.Log("Exited Shooting Phase");
    }
    private void HandleShootingMovement()
    {
        Vector2 moveDir = moveAction.ReadValue<Vector2>();
        // W/S adjust shooting direction
        if (moveDir.y > 0){
            pl.gunCom.shootingDirection += 0.1f;
        }
        if (moveDir.y < 0){
            pl.gunCom.shootingDirection -= 0.1f;
        }

        // A/D adjust player's face direction
        if (moveDir.x != 0){
            // Detect double-tap to exit shooting phase
            float currentTime = Time.time;
            if (currentTime - lastTapTime <= doubleTapThreshold)
            {
                EnterMovingPhase();
            }
            else
            {
                lastTapTime = currentTime;
            }

            if (moveDir.x > 0){pl.FaceRight();}
            else if (moveDir.x < 0){pl.FaceLeft();}
        }
    }
}

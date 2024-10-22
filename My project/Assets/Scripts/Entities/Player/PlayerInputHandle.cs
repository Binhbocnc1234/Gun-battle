using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Observer;



public class PlayerInputHandle : MonoBehaviour
{
    Player pl;
    public PlayerInput playerInput;
    public InputAction moveAction, shotAction, jumpAction, lootAction;
    public InputActionMap inputActionMap;
    GunShooting gunShooting;
    Timer jumpDelay = new Timer(0.4f);
    Timer holdingTimer = new Timer(0.3f);
    private bool isHoldingShootingKey;
    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f;  // Time window for double tap
    void Awake(){
        pl = GetComponent<Player>();
        playerInput = new PlayerInput();
        if (pl.index == 1){
            inputActionMap = playerInput.Player_2;
        }
        else if (pl.index == 0){
            inputActionMap = playerInput.Player;
        }
        moveAction = inputActionMap.FindAction("Move", true);
        shotAction = inputActionMap.FindAction("Shot", true);
        jumpAction = inputActionMap.FindAction("Jump", true);
        lootAction = inputActionMap.FindAction("Loot", true);
        inputActionMap.Enable();
        ExitShootingPhase();
    }
    void Update(){
        if (ControllerPvP.instance.activeGameState == (int)BattleState.Start || pl.isOnDeath){return;}
        jumpDelay.Count(false);
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
            pl.moveDir = context.ReadValue<Vector2>();
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
    //Haven't used
    public void OnJump(InputAction.CallbackContext context)
    {
        if (jumpDelay.Count(false) && pl.isGrounded()){
            jumpDelay.Reset();
            pl.Jump();
        }
    }

    public void OnLootOrSitDown(InputAction.CallbackContext context)
    {
        if (context.performed && !pl.isShooting){
            pl.Loot();
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
        lootAction.canceled -= OnLootOrSitDown;
        Debug.Log("Entered Shooting Phase");
    }

    private void ExitShootingPhase()
    {
        pl.isShooting = false;
        shotAction.canceled += OnShoot;
        moveAction.performed += OnMove;
        // moveAction.performed -= HandleShootingMovement;
        jumpAction.started += OnJump;
        lootAction.canceled += OnLootOrSitDown;
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
                ExitShootingPhase();
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

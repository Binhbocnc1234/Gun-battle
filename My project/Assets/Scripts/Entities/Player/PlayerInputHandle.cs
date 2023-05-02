using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;



public class PlayerInputHandle : MonoBehaviour
{
    Player _pl;
    GunShooting _gunShooting;
    Timer jumpDelay = new Timer(0.4f);
    // Start is called before the first frame update
    void Update(){
        if (ControllerPvP.instance.activeGameState == (int)BattleState.Start || _pl.isOnDeath){return;}
        _pl.moveDir = _pl.moveAction.ReadValue<Vector2>();
        if ((_pl.moveDir.x == 1 && !_pl.isFacingRight) || (_pl.moveDir.x == -1 && _pl.isFacingRight)){
            _pl.Flip();
        }
        if (_pl.moveDir.x != 0){
            _pl.transform.Translate((new Vector3(1,0,0))*_pl.moveDir*_pl.movingSpeed*Time.deltaTime);
        }
        
        if (_pl.shotAction.ReadValue<float>() > 0.1f){
            // if (_pl.index == 1){
            //     this.PostEvent(EventID.OnFiPlayerShot, _pl);
            // }
            _pl.gunCom.GetComponent<GunShooting>().Shot();
        }
        if (_pl.lootAction.triggered){
            _pl.Loot();
            // if (_pl.index == 1){
            //     this.PostEvent(EventID.OnFiPlayerLoot, _pl);
            // }
            // else if (_pl.index == 2){
            //     this.PostEvent(EventID.OnSePlayerLoot, _pl);
            // }
        }
        if (jumpDelay.Count(false) && _pl.jumpAction.triggered && _pl.isGrounded()){
            jumpDelay.Reset();
            _pl.Jump();
        }
    }
    void Awake(){
        _pl = GetComponent<Player>();
    }
    void Start(){
        _gunShooting = _pl.gunCom.GetComponent<GunShooting>();
    }
}

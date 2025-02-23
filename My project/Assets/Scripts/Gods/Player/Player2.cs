using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public partial class Player : Entity{
    //State : idle, move, jump, shoot, sit down
    [HideInInspector] public bool isOnDeath = false, isOnRevive = false, isMoving = false, 
    isSitDown = false, isGrounded = true;
    private bool _isShooting;
    [HideInInspector] public bool isShooting{
        get{return _isShooting;}
        set{
            _isShooting = value;
            if (value){gunCom.gameObject.SetActive(false);}
            else{gunCom.gameObject.SetActive(true);}
        }
    }
    public bool canPerformDoubleJump;
    private bool _isJumping;
    public bool isJumping{
        get{return _isJumping;} 
        set{
            _isJumping = value;
            if (value){canPerformDoubleJump = true; isGrounded = false;}
            else{canPerformDoubleJump = false;}
    }}
    void HandlingAnimator(){
        if (isJumping){
            animator.Play("Jump");
        }
        else if(isMoving){
            animator.Play("Move");
        }
        else if(isSitDown){
            animator.Play("SitDown");
        }
        else{
            animator.Play("Idle");
        }
    }
    void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.tag == Tag.Wall){
            isGrounded = true;
            isJumping = false;
        }
        
    }
    void OnCollisionEnter2D(Collision2D other){
        isGrounded = true;
        isJumping = false;
    }
    void OnCollisionExit2D(Collision2D other){
        
    }
}
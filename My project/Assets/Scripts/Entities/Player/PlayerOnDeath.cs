using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class perform following actions when Player health is below zero: <br/>
/// +Phase 1: Turn off these components : SpriteRenderer, RigidBody <br/>
/// +Phase 2: Player gets temporary immunity effect: Player can't be damaged and turn yellow
/// </summary>
[RequireComponent(typeof(Player))]
public class PlayerOnDeath : MonoBehaviour
{
    Player _pl;
    [HideInInspector] public Timer deathTimer = new Timer(2.0f), immuneTimer = new Timer(2.0f);
    void Start()
    {
        _pl = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pl.isOnDeath && deathTimer.Count()){
            OnRevive();
        }
        if (_pl.isOnRevive && immuneTimer.Count()){
            _pl.immuneRate = 0;
            _pl.isOnRevive = false;
        }
    }
    // OnDealth() : from Player
    ///<sumary>
    ///After disappearing, Player respawns in random position, weapon is reseted to default, Player is immortal for a short time.
    ///<sumary>
    void OnRevive(){
        _pl.isOnDeath = false; _pl.isOnRevive = true;
        int posInd = Random.Range(0, ObjectHolder.instance.playerSpawnPosition.childCount);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        _pl.gunContainer.gameObject.SetActive(true);
        transform.position = ObjectHolder.instance.playerSpawnPosition.GetChild(posInd).position;
        _pl.immuneRate = 1;
        _pl.health = _pl.mainHealth;
        _pl.armor = 0;
        _pl.SwitchGun(ControllerPvP.instance.defaultGun);
    }
    void OnDeath(){
        _pl.isOnDeath = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        _pl.gunContainer.gameObject.SetActive(false);
    }
}

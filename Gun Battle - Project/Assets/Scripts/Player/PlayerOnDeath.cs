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
    private Player m_player;
    private PVPConfig pvpConfig;
    void Start()
    {
        m_player = GetComponent<Player>();
        m_player.OnDeath += StartOnReviveCoroutine;
        pvpConfig = PVPConfig.GetPVPConfig();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void StartOnReviveCoroutine()
    {
        StartCoroutine(OnReviveCoroutine());
    }
    // OnDealth() : from Player
    ///<summary>
    ///After disappearing, Player respawns in random position, weapon is reseted to default, Player is immortal for a short time.
    ///</summary>
    IEnumerator OnReviveCoroutine()
    {
        Disappear();
        yield return new WaitForSeconds(2.0f);
        Revive();
        yield return new WaitForSeconds(2.0f);
        EndImmune();
    }
    void Disappear()
    {
        m_player.isOnDeath = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        m_player.gunContainer.gameObject.SetActive(false);
        m_player.SwitchGun(pvpConfig.defaultGun);
    }
    void Revive()
    {
        int respawnInd = Random.Range(0, ObjectHolder.instance.playerSpawnPosition.childCount);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        m_player.gunContainer.gameObject.SetActive(true);
        transform.position = ObjectHolder.instance.playerSpawnPosition.GetChild(respawnInd).position;
        m_player.immuneRate = 1;
    }
    void EndImmune()
    {
        m_player.immuneRate = 0;
        m_player.Reset();
    }
}

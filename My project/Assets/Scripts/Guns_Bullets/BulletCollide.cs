using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class BulletCollide : MonoBehaviour
{
    Bullet _bullet;
    [SerializeField] Transform explodeAnim;
    [HideInInspector] public bool isExplode = false;
    public int explodeDamage = 0;
    public float explodeRadius = 0;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _bullet = GetComponent<Bullet>();
        if (explodeAnim == null){
            explodeAnim = ObjectHolder.instance.rifleExplode;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        Player playerCom = other.GetComponent<Player>();
        Explode();
        if (other.tag == "Wall"){ //The bullet collided with Wall
            Destroy();
        }
        if (playerCom != null && playerCom.group != _bullet.team){
            playerCom.GetDamage(_bullet.damage);
            if (_bullet.strikeThroughLeft == 0){Destroy();}
            else{_bullet.strikeThroughLeft -=1;}
        }
    }
    void Explode(){
        foreach(Transform obj in ObjectHolder.instance.playerContainer){
            Player pl = obj.GetComponent<Player>();
            // Vector2.Vector2
            if (pl.group != _bullet.team && Vector2.Distance(pl.transform.position, transform.position) <= explodeRadius){
                pl.GetDamage(explodeDamage);
            }
        }
    }
    public void Destroy(){
        Transform explodeObj = Instantiate(explodeAnim, transform.position, transform.rotation);
        explodeObj.transform.localScale = this.transform.localScale;
        explodeObj.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
}

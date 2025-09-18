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
        Entity entity = other.GetComponent<Entity>();
        if (other.tag == "Wall"){
            Explode();
            Destroy();
        }
        else if (entity != null && entity.group != _bullet.team){
            Explode();
            //Create blood effect
            Vector3 dir = _bullet.owner.transform.position - this.transform.position;
            BloodSplashManager.Instance.CreateBloodSplash(other.transform, dir);
            entity.GetDamage(_bullet.damage);
            if (_bullet.strikeThroughLeft == 0){Destroy();}
            else{_bullet.strikeThroughLeft -=1;}
        }
    }
    void Explode(){
        foreach(Transform obj in ObjectHolder.instance.playerContainer){
            Entity entity = obj.GetComponent<Entity>();
            if (entity.group != _bullet.team){
                if (Vector2.Distance(entity.transform.position, transform.position) <= explodeRadius){
                    entity.GetDamage(explodeDamage);
                }
            }
            else{
                if (Vector2.Distance(entity.transform.position, transform.position) <= explodeRadius){
                    entity.GetDamage(explodeDamage/2);
                }
            }
        }
    }
    public void Destroy(){
        Transform explodeObj = Instantiate(explodeAnim, transform.position, transform.rotation);
        if (transform.localScale.x < 0){
            var localScale = explodeObj.localScale;
            localScale.x *= -1;
            explodeObj.localScale = localScale;
        }
        explodeObj.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
}

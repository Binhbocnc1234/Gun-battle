using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    protected Gun m_gun;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_gun = GetComponent<Gun>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    ///<summary>
    ///Instantiate a Bullet and make it fly in specified direction
    ///</summary>
    public virtual void Shot(){
        if (m_gun.fireRate.isEnd && m_gun.reloadTime.isEnd){
            m_gun.fireRate.Reset();
            Vector3 angle = m_gun.owner.transform.rotation.eulerAngles;
            angle.z += m_gun.shootingDirection;
            Quaternion rotation = Quaternion.Euler(m_gun.owner.transform.rotation.eulerAngles + 
            new Vector3(0,0, Random.Range(-m_gun.inaccuracy,+m_gun.inaccuracy)));
            m_gun.SpawnBullet(m_gun.owner.transform.position + m_gun.owner.transform.rotation.x*Gun.shotdiff, rotation);
            if (m_gun.magazineCapacity != 0){
                m_gun.remainingBullet--;
                if (m_gun.remainingBullet == 0){
                    m_gun.StartReload();
                }
            }
            m_gun.audioManager.Play("Shot");
        }
    }
}
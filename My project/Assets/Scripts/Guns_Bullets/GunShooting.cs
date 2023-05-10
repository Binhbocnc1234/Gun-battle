using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    protected Gun m_gun;
    protected Player m_pl;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_gun = GetComponent<Gun>();
        m_pl = m_gun.player;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    public virtual void Shot(){
        if (m_gun.fireRate.isEnd && m_gun.reloadTime.isEnd){
            m_gun.fireRate.Reset();
            Quaternion rotation = Quaternion.Euler(m_pl.transform.rotation.eulerAngles + (new Vector3(0,0, Random.Range(-m_gun.inaccuracy,+m_gun.inaccuracy))));
            m_gun.SpawnBullet(m_pl.transform.position + m_pl.transform.rotation.x*Gun.shotdiff, rotation);
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

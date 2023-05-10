using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShooting : GunShooting
{
    // Start is called before the first frame update
    public int bulletPerShot = 4;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    ///<summary>
    /// Shot multiple bullets at the same time. Bullets have random angle formed by the x-axis and trajectory of the bullet
    ///</summary>
    public override void Shot(){
        if (m_gun.fireRate.isEnd && m_gun.reloadTime.isEnd){
            m_gun.fireRate.Reset();

            //Shot multiple bullets at same time
            for(int i = 0; i < bulletPerShot; ++i){
                Bullet bul = m_gun.SpawnBullet(m_pl.transform.position + m_pl.transform.rotation.x*Gun.shotdiff, m_pl.transform.rotation);
                bul.transform.Rotate(new Vector3(0,0, Random.Range(-m_gun.inaccuracy, +m_gun.inaccuracy)));
            }

            m_gun.SpawnBullet(m_pl.transform.position + m_pl.transform.rotation.x*Gun.shotdiff, m_pl.transform.rotation);
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

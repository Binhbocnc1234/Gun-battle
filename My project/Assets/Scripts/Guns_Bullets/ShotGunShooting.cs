using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShooting : GunShooting
{
    // Start is called before the first frame update
    public int bulletPerShot = 4;
    public int maxDiff = 30;

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
        if (_gun.fireRate.isEnd && _gun.reloadTime.isEnd){
            _gun.fireRate.Reset();

            //Shot multiple bullets at same time
            for(int i = 0; i < bulletPerShot; ++i){
                Bullet bul = _gun.SpawnBullet(_player.transform.position + _player.transform.rotation.x*Gun.shotdiff, _player.transform.rotation);
                bul.transform.Rotate(new Vector3(0,0, Random.Range(-maxDiff, +maxDiff)));
            }

            _gun.SpawnBullet(_player.transform.position + _player.transform.rotation.x*Gun.shotdiff, _player.transform.rotation);
            if (_gun.magazineCapacity != 0){
                _gun.remainingBullet--;
                if (_gun.remainingBullet == 0){
                    _gun.StartReload();
                }
            }
            _gun.audioManager.Play("Shot");
        }
    }
}

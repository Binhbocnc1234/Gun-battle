using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    protected Gun _gun;
    protected Player _player;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _gun = GetComponent<Gun>();
        _player = _gun.player;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    public virtual void Shot(){
        if (_gun.fireRate.isEnd && _gun.reloadTime.isEnd){
            _gun.fireRate.Reset();
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

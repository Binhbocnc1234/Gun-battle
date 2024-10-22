using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Gun")]
public class GunModel : ScriptableObject
{
    //Identity
    public string gunName;public GunType gunType;
    //Appearance
    public Sprite model;
    public Sprite textureInBattleField;
    //About gun
    public float bulletDelay, reloadDelay;
    public float inaccuracy;
    public int magazineCapacity;
    //About bullet
    public int damage;
    public int bulletMovingSpeed;
    public int armorPenetration;
    public int explodeDamage;
    public float explodeRadius;
    //Level up
    public int damageUp;
    public float fireRateUp;
    public int magazineCapacityUp;

}

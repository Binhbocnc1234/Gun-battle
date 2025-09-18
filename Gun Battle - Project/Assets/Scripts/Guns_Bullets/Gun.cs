using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 shotdiff = new Vector3(-0.1f, 0.3f, 0); //Value represents accuracy of gun
    public GunModel gunModel;
    public eGroup group;
    [HideInInspector] public Transform owner;
    [HideInInspector] public int level = 0, maxLevel = 3;
    [HideInInspector] public Bullet bullet;
    [HideInInspector] public AudioManager audioManager;
    public string gunName;
    [HideInInspector] public float bulletDelay, reloadDelay, inaccuracy;
    public Timer fireRate = new Timer(0); public Timer reloadTime = new Timer(0);
    [HideInInspector] public bool isReloadTime = false;
    [HideInInspector] public int magazineCapacity; [HideInInspector] public int remainingBullet; protected int bulletInMag;
    [HideInInspector] public float shootingDirection;
    protected virtual void Start()
    {   
        //Initialization for Gun
        
        foreach(Transform child in transform){
            if (child.name == "Bullet"){
                bullet = child.GetComponent<Bullet>();
                bullet.gameObject.SetActive(false);
            }
            else if(child.name == "AudioManager"){
                audioManager = child.GetComponent<AudioManager>();
                audioManager.gameObject.SetActive(true);
            }
        }
        //Copy values from SO to Gun Component

        gunName = bullet.gunName = gunModel.gunName;
        GetComponent<SpriteRenderer>().sprite = gunModel.textureInBattleField;
        fireRate.totalTime = fireRate.curTime = gunModel.bulletDelay; 
        reloadTime.totalTime = gunModel.reloadDelay;
        inaccuracy = gunModel.inaccuracy;
        magazineCapacity = gunModel.magazineCapacity;
        remainingBullet = magazineCapacity;
        bullet.damage = gunModel.damage;
        bullet.armorPen = gunModel.armorPenetration;
        bullet.movingSpeed = gunModel.bulletMovingSpeed;
        //ReloadAnim
        Vector3 temp = transform.position;
        temp.y -= 1.5f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Debug.Log($"level : {level}, magazine : {magazineCapacity}");
        fireRate.Count(false);
        fireRate.totalTime = gunModel.bulletDelay - gunModel.fireRateUp * level;
        bullet.damage = gunModel.damage + gunModel.damageUp * level;
        magazineCapacity = gunModel.magazineCapacity + gunModel.magazineCapacityUp * level;
        if (isReloadTime){
            if (reloadTime.Count(false)){
                EndReload();
            }
        }
    }
    public virtual Bullet SpawnBullet(Vector3 pos, Quaternion rotation){
        Bullet bul = Instantiate(bullet, pos, rotation);
        bul.transform.localScale = transform.lossyScale;
        bul.gameObject.SetActive(true);
        bul.gunName = gunName;
        //bul.owner = player.entity;
        return bul;
    }
    public virtual void StartReload(){
        isReloadTime = true;
        //player.reloadAnim.gameObject.SetActive(true);
        reloadTime.Reset();
        audioManager.Play("Reload");
    }
    ///<summary>
    ///Turn off reloading amination and reset remainingBullet
    ///</summary>
    public virtual void EndReload(){
        // Debug.Log("Reload has finished");
        isReloadTime = false;
        //player.reloadAnim.gameObject.SetActive(false);
        remainingBullet = magazineCapacity;
    }
    public void Reset(){
        reloadTime.Reset();
        fireRate.Reset();
        remainingBullet = bulletInMag;
    }
    public void LevelUp(){
        level++;
        level = Mathf.Min(level, maxLevel);
    }
    public virtual string GetName()
    {
        string i = "";
        for (int j = 0; j < level; ++j)
        {
            i = i + "I";
        }
        return gunName + " - " + i;
    }
}

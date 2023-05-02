using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 shotdiff = new Vector3(0, 0.2f, 0);
    [HideInInspector] public Player player;
    [HideInInspector] public Bullet bullet;
    [HideInInspector] public AudioManager audioManager;
    public string gunName;
    public float bulletDelay, reloadDelay;
    public Timer fireRate = new Timer(0); public Timer reloadTime = new Timer(0);
    [HideInInspector] public bool isReloadTime = false;
    public int magazineCapacity; [HideInInspector] public int remainingBullet; protected int bulletInMag;


    protected virtual void Start()
    {
        fireRate.totalTime = fireRate.curTime = bulletDelay; reloadTime.totalTime = reloadDelay;
        remainingBullet = magazineCapacity;
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
        
        //ReloadAnim
        Vector3 temp = transform.position;
        temp.y -= 1.5f;
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        fireRate.Count(false);
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
        bul.team = player.group;
        bul.owner = player;
        return bul;
    }
    public virtual void StartReload(){
        isReloadTime = true;
        player.reloadAnim.gameObject.SetActive(true);
        reloadTime.Reset();
        audioManager.Play("Reload");
    }
    public virtual void EndReload(){
        // Debug.Log("Reload has finished");
        isReloadTime = false;
        player.reloadAnim.gameObject.SetActive(false);
        remainingBullet = magazineCapacity;
    }
}

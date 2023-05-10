using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public string gunName;
    public float movingSpeed;
    public int damage = 0;
    public float armorPen;
    public bool isReflect;
    public int strikeThroughLeft = 0;
    [HideInInspector] public int portalPassLeft;
    [HideInInspector] public string team;
    [HideInInspector] public Entity owner;

    // Update is called once per frame
    void Start(){
        portalPassLeft = strikeThroughLeft + 1;
    }
    void Update()
    {
        
    }
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public string gunName;
    [HideInInspector] public float movingSpeed;
    [HideInInspector] public int damage = 0;
    [HideInInspector] public float armorPen;
    [HideInInspector] public bool isReflect;
    [HideInInspector] public int strikeThroughLeft = 0;
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


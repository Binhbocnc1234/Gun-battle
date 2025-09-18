using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    Bullet bul;
    void Start()
    {
        bul = GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveStraight();
    }
    void MoveStraight(){
        transform.Translate((new Vector3(1,0,0))*transform.lossyScale.x*bul.movingSpeed*Time.deltaTime);
    }
}

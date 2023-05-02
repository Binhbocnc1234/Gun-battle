using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static float different = 1f;
    public bool isVisible = false;
    public Portal partner;
    public Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        if (isVisible){
            foreach(Transform child in transform){
                child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.GetComponent<Player>() != null){
            Vector3 tmp = partner.transform.position;
            tmp += dir;
            other.transform.position = tmp;
            
        }
    }
    void Hiding(){
        isVisible = false;
        foreach(Transform child in transform){
            child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}

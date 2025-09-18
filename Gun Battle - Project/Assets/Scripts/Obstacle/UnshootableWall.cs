using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnshootableWall : MonoBehaviour
{

    // Start is called before the first frame update
    new BoxCollider2D collider;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            Player pl = other.GetComponent<Player>();
            BoxCollider2D co = pl.GetComponent<BoxCollider2D>();
            // if (co.bounds.extents.)
        }
    }
}

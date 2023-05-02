using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollower : MonoBehaviour
{

    [HideInInspector] public int zoomLevel;
    [HideInInspector] public Vector3 nextPosition;
    [HideInInspector] public float speed = 0.125f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, nextPosition, speed);
    }

}

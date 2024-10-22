using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    //For smooth camera movement
    public bool smoothMovement = true;
    [HideInInspector] public Vector3 nextPosition;
    public float speed = 0.125f;
    //For smooth camera resize
    public bool smoothResize = true;
    [HideInInspector] public float targetSize;
    public float smoothTime = 0.3f;  // The time it takes to smooth resize (lower = faster)
    private float currentVelocity = 0.0f; // Reference for SmoothDamp's velocity calculation
    private float original_z;
    void Start(){
        targetSize = Camera.main.orthographicSize;
        original_z = Camera.main.transform.position.z;
    }
    void Update()
    {
        if (smoothMovement){
            transform.position = Vector3.Lerp(transform.position, nextPosition, speed);
        }
        
        if (smoothResize){
            Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetSize, 
            ref currentVelocity, smoothTime);
        }
    }

}

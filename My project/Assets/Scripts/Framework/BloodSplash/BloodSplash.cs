using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplash : MonoBehaviour
{
    public GameObject particlePrefab;
    public Transform target;
    public float force = 5f; 
    public float bloodLifetime = 5.0f; 
    public Timer effectTimer = new Timer(0.7f);
    public Timer particleDelay = new Timer(0.1f); //How long is the interval between two particle spawnings?
    [HideInInspector] public Vector2 mainDirection{get; private set;} //mainDirection should have magnitude of 1
    void Update(){
        if (target != null){
            this.transform.position = target.position;
        }
        if (particleDelay.Count()){
            SpawnBlood(this.transform.position);
        }
        if(effectTimer.Count()){Destroy(this.gameObject);}
    }
    public void SpawnBlood(Vector3 spawnPosition){
        GameObject blood = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
        blood.transform.SetParent(this.transform);
        Vector2 randomDirection = mainDirection + new Vector2(
            Random.Range(-1f, 1f), 
            Random.Range(-1f, 1f)
        ).normalized;

        // Add Rigidbody and apply force on blood particle
        Rigidbody rb = blood.AddComponent<Rigidbody>();
        rb.useGravity = true; // Để hạt máu chịu tác động của trọng lực
        rb.AddForce(randomDirection * force, ForceMode.Impulse);

        // Destroy blood particles after a period of time
        Destroy(blood, bloodLifetime);
    }
    public void SetDirection(Vector3 direction){
        mainDirection = Vector3.Normalize(direction);
    }
}

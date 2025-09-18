using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    protected static ObjectHolder _instance;
    public static ObjectHolder instance{get => _instance;}
    public Transform playerContainer;
    public Transform itemList;
    public Transform gunSet;
    public Transform rifleExplode;
    public Transform battleStates;
    public Transform playerSpawnPosition;
    // Start is called before the first frame update
    void Awake(){
        if (_instance == null){
            _instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

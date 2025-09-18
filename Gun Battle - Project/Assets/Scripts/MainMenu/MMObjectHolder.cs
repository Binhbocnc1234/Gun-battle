using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMObjectHolder : MonoBehaviour
{
    // Start is called before the first frame update
    protected static MMObjectHolder _instance;
    public static MMObjectHolder instance{get => _instance;}
    public Canvas canvas_main, canvas_setting, canvas_control;
    public static float camWidth, camHeight;
    void Awake()
    {
        _instance = this;
        
    }
    void Start(){
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

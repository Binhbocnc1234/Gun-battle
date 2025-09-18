using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SettingButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas currentCanvas;
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
    public void MoveToAnotherCanvas(Canvas nextCanvas){
        currentCanvas.gameObject.SetActive(false);
        nextCanvas.gameObject.SetActive(true);
    }
    
}

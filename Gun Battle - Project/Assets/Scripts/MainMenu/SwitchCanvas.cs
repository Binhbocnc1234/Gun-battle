using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveToAnotherCanvas(Canvas currentCanvas, Canvas nextCanvas){
        currentCanvas.gameObject.SetActive(false);
        nextCanvas.gameObject.SetActive(true);
    }
}

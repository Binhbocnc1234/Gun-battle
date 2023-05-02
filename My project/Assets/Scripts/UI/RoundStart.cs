using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoundStart : MonoBehaviour
{
    public TextMeshProUGUI remainingTimeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        remainingTimeText.text = ((int)ControllerPvP.instance.infoTimer[0].remainingTime + 1).ToString();
    }
}

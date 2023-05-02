using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoundEnd : MonoBehaviour
{
    public TextMeshProUGUI roundText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        roundText.text = string.Format("{0} won the round!", ControllerPvP.instance.playerName[ControllerPvP.instance.winner]);
    }
}

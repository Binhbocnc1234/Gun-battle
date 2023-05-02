using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreBar : MonoBehaviour
{
    // Start is called before the first frame update
    // public 
    public TextMeshProUGUI objective, player_1, player_2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objective.text = ControllerPvP.instance.objective.ToString();
        player_1.text = ControllerPvP.instance.score[0].ToString();
        player_2.text = ControllerPvP.instance.score[1].ToString();
    }
}

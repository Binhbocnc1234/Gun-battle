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
        objective.text = PVPConfig.GetPVPConfig().objective.ToString();
    }

    // Update is called once per frame
    public void UIUpdate()
    {

    }
    public void UpdateInfo(int firstScore, int secondScore)
    {
        player_1.text = firstScore.ToString();
        player_2.text = secondScore.ToString();
    }
}

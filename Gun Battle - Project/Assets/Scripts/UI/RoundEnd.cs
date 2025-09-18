using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoundEnd : Singleton<RoundEnd>
{
    public TextMeshProUGUI roundText;
    public void UpdateInfo(int playerIndex)
    {
        this.gameObject.SetActive(true);
        roundText.text = $"Player {playerIndex} won the battle!";
    }
}

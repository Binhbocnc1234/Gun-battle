using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public Scrollbar scrollbar;
    public TextMeshProUGUI healthText, bulletText;
    // private healthText
    void Start(){

    }

    // Update is called once per frame
    void Update()
    {
        // textObj.text
        if (player != null){
            healthText.text = string.Format("HP: {0}/{1}", player.health, player.mainHealth);
            bulletText.text = string.Format("{0}/{1}", player.gunCom.remainingBullet, player.gunCom.magazineCapacity);
            scrollbar.size = (float)player.health/ player.mainHealth;
        }
        else{
            
        }
        
    }
    void OnPlayerInfoChange(object info){
        
    }
}

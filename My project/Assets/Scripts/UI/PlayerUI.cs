using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public RectTransform healthFill;
    public TextMeshProUGUI healthText, bulletText;
    float mainWidth;
    // private healthText
    void Start(){
        mainWidth = healthFill.GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        // textObj.text
        if (player != null){
            healthText.text = string.Format("HP: {0}/{1}", player.health, player.mainHealth);
            bulletText.text = string.Format("{0}/{1}", player.gunCom.remainingBullet, player.gunCom.magazineCapacity);
            healthFill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, mainWidth*(((float)player.health)/player.mainHealth));
        }
        else{
            Debug.LogWarning($"There is no player attached to {this.name}");
        }
        
    }
    void OnPlayerInfoChange(object info){
        
    }
}

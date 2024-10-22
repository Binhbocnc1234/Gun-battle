using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState{Fighting = -1, Start = 0, BattleWinning = 1, End = 2};
public class ControllerPvP : MonoBehaviour
{
    // Base fields
    protected static ControllerPvP _instance;
    public static ControllerPvP instance{get => _instance;}
    [HideInInspector] public float camHeight, camWidth;
    // Battle infomation
    public float gravity = 9.8f;
    public string defaultGun = "Colt 45";
    public int usedMap = 0, limitTime = 0; //limitTime in seconds
    public int objective = 5;
    [HideInInspector] public int[] usedSkin = new int[2];
    [HideInInspector] public string[] playerName = new string[2]{"teamA", "teamB"};
    [HideInInspector] public int roundPassed = 0;
    [HideInInspector] public int winner;
    [HideInInspector] public int[] score = new int[2]{0, 0};
    //RoundInfo
    [HideInInspector] public Timer[] infoTimer = new Timer[]{new Timer(4), new Timer(4), new Timer(2)};
    [HideInInspector] public int activeGameState = -1;
    void Awake(){
        if (_instance != null){
            Debug.LogWarning("You have created another object of same type 'ControllerPvP'");
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }
    }
    void Start()
    {
        camHeight  = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
        RoundStart();
    }

    // Update is called once per frame
    void Update()
    {

        ConditionToEndState();
    }
    public void RoundStart(){
        SetActiveInfo(0);
    }
    public void ConditionToEndState(){
        if (activeGameState != -1 && infoTimer[activeGameState].Count()){
            EndState();
        }
    }
    // public void ConditionToRoundEnd(){
    //     if (activeGameState == (int)BattleState.Fighting){
    //         Transform plContainer = ObjectHolder.instance.playerContainer;
    //         for(int i = 0; i <  plContainer.childCount; ++i){
    //             Player playerCom = plContainer.GetChild(i).GetComponent<Player>();

    //             if (playerCom.health == 0 && playerCom.isOnDeath == false){
    //                 RoundEnd(1-i);
    //             }
    //         }
    //     }
    // }
    public void RoundEnd(int winnerInd){
        // activeGameState = (int)BattleState.End;
        // roundPassed += 1;
        this.winner = winnerInd;
        score[winnerInd]++;
        if (score[winnerInd] == objective){
            BattleWinning();
        }
    }
    public void BattleWinning(){
        SetActiveInfo((int)BattleState.BattleWinning);
        ObjectHolder.instance.playerContainer.GetChild(winner).GetComponent<Player>().immuneRate = 1 ;
        SmoothCamera camFollow = Camera.main.GetComponent<SmoothCamera>();
    }
    public void SetActiveInfo(int index){
        foreach(Transform child in ObjectHolder.instance.battleStates){
            child.gameObject.SetActive(false);
        }
        if (index != -1){
            Debug.Log(ObjectHolder.instance.battleStates.GetChild(index).name);
            ObjectHolder.instance.battleStates.GetChild(index).gameObject.SetActive(true);
        }
        
        activeGameState = index;
    }
    public void EndState(){ //The function will be implemented after a State is finished
        if (activeGameState == 0){ //After BattleStart
            SetActiveInfo(-1);
            return;
        }
        else if (activeGameState == 1){ //After BattleWinning
            SceneManager.LoadScene("MainMenu");
        }
    }
}

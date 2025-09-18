using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState{Fighting = -1, Start = 0, BattleWinning = 1, End = 2};
public enum Tag{Wall, Bullet, Player, Entity}
public class ControllerPvP : MonoBehaviour
{
    // Singleton
    protected static ControllerPvP _instance;
    public static ControllerPvP instance{get => _instance;}
    [HideInInspector] public float camHeight, camWidth;
    // Battle infomation
    public float gravity = 9.8f;
    public string defaultGun = "Colt 45";
    public int usedMap = 0, limitTime = 0; //limitTime in seconds
    public int objective = 5;
    [HideInInspector] public int roundPassed = 0;
    [HideInInspector] public int winner;
    [HideInInspector] public int[] score = new int[2]{0, 0};
    //RoundInfo
    [HideInInspector] public Timer[] infoTimer = new Timer[]{new Timer(4), new Timer(4), new Timer(2)};
    [HideInInspector]
    public BattleState activeGameState = BattleState.Fighting;
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
        int ind = 0;
        foreach (Player pl in Player.container)
        {
            pl.SwitchGun(defaultGun);
            pl.index = ind;
            pl.OnPlayerDeath += RoundEnd;
            ind++;
        }
        RoundStart();
    }

    // Update is called once per frame
    void Update(){
        ConditionToEndState();
        foreach (Player pl in Player.container) //Lỗi: không có tham chiếu tại Player.container
        {
            if (pl.isAlive == false)
            {
                RoundEnd(1 - pl.index);
            }
        }
    }
    public void RoundStart(){
        SetActiveInfo(0);
    }
    public void ConditionToEndState(){
        if (activeGameState != BattleState.Fighting && infoTimer[(int)activeGameState].Count()){
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
        Player.container[1 - winnerInd].GetComponent<PlayerInputHandle>().enabled = false;
        score[winnerInd]++;
        if (score[winnerInd] == objective){
            BattleWinning();
        }
    }
    public void BattleWinning()
    {
        SetActiveInfo(BattleState.BattleWinning);
        Player.container[winner].GetComponent<Entity>().immuneRate = 1;
        SmoothCamera camFollow = Camera.main.GetComponent<SmoothCamera>();
    }
    public void SetActiveInfo(BattleState state){
        foreach(Transform child in ObjectHolder.instance.battleStates){
            child.gameObject.SetActive(false);
        }
        if (state != BattleState.Fighting){
            // Debug.Log(ObjectHolder.instance.battleStates.GetChild(state).name);
            // ObjectHolder.instance.battleStates.GetChild(state).gameObject.SetActive(true);
        }
        
        activeGameState = state;
    }
    /// <summary>
    /// The function will be implemented after a State is finished
    /// </summary>
    public void EndState()
    { 
        if (activeGameState == BattleState.Start)
        { //After BattleStart
            SetActiveInfo(BattleState.Fighting);
            return;
        }
        else if (activeGameState == BattleState.BattleWinning)
        { //After BattleWinning
            SceneManager.LoadScene("MainMenu");
        }
    }
}

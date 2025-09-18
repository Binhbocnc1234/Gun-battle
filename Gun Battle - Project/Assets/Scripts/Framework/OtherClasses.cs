using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchElement<T>{
    public List<T> list;
    public int curIndex = 0;
    public SwitchElement(List<T> list){
        this.list = list;
    }
    public T Switch(){
        curIndex +=1;
        if (curIndex > list.Count-1){
            curIndex = 0;
        }
        return GetElement();
        
    }
    public T GetElement(){
        return list[curIndex];
    }
}
public class Timer{
    public float curTime=0;
    public float remainingTime{get; private set;}
    public float totalTime;
    public bool isEnd = true;
    public Timer(float totalTime){
        this.totalTime = totalTime;
    }
    public Timer()
    {
        
    }
    public bool Count(bool reset = true){
        
        if (curTime >= totalTime){
            if (reset){curTime = curTime - totalTime;}
            else{curTime = totalTime;}
            isEnd = true;
        }
        else{
            curTime+=Time.deltaTime;
            isEnd = false;
        }
        remainingTime = totalTime - curTime;
        return isEnd;
    }
    public void Reset(){
        curTime =0;
        isEnd = false;
    }
}

public class ArrayLayout<type>{
    public type name;
}



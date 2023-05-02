using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ListCom : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public Transform selectedObj = null;
    [HideInInspector] public int selectedIndex = -1;

    void Start()
    {
        SwitchElement(selectedIndex);
    }

    void Update()
    {
        
    }
    public void SwitchElement(string name, Func<Transform, string> func = null){
        if (func == null){func = tr => tr.name;}
        bool isSwitched = false;
        for (int i = 0; i < transform.childCount; ++i){
            Transform child = transform.GetChild(i);
            if (func(child) == name){
                child.gameObject.SetActive(true);
                selectedIndex = i;
                selectedObj = child.transform;
                isSwitched = true;
            }
            else{
                child.gameObject.SetActive(false);
            }
        }
        if(!isSwitched){Debug.LogWarning(string.Format("Cannot find element named '{0}'", name));}
    }
    public void SwitchElement(int index){
        for (int i = 0; i < transform.childCount; ++i){
            GameObject child = transform.GetChild(i).gameObject;
            if (index == i){
                child.SetActive(true);
                selectedIndex = i;
                selectedObj = child.transform;
            }
            else{
                child.SetActive(false);
            }
        }
        if(index >= transform.childCount){Debug.LogWarning(string.Format("Cannot find element named {}", name));}
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(ListCom))]
    public class ListComEditor : Editor{
        public override void OnInspectorGUI(){
            base.OnInspectorGUI();
            ListCom listCom = (ListCom)target;
            listCom.selectedIndex = EditorGUILayout.IntField("Choosen gun's index", listCom.selectedIndex);
            if (GUILayout.Button("Switch gun")){
                listCom.SwitchElement(listCom.selectedIndex);
            }
        }
    }
    #endif 
}

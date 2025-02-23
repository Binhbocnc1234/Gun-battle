using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BloodSplashManager : Singleton<BloodSplashManager>{

    public BloodSplash bloodSplashPrefab;
    public void CreateBloodSplash(Transform target, Vector2 mainDirection, float timer = 0.7f, 
        float particleDelay = 0.2f, float force = 5f){
        GameObject child = Instantiate(bloodSplashPrefab.gameObject, target);
        child.SetActive(true);
        BloodSplash bloodSplash = child.GetComponent<BloodSplash>();
        bloodSplash.target = target;
        bloodSplash.SetDirection(mainDirection);
        bloodSplash.effectTimer.totalTime = timer;
        bloodSplash.particleDelay.totalTime = particleDelay;
        bloodSplash.force = force;
        child.transform.SetParent(transform);
    }
    void Start(){
        bloodSplashPrefab.gameObject.SetActive(false);
    }
    void Update(){
        if (Input.GetMouseButtonUp(0)){
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
            CreateBloodSplash(this.transform, new Vector2(0, 1));
        }
    }
}
[CustomEditor(typeof(BloodSplashManager))]
[CanEditMultipleObjects]
public class MyComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Display the "SingleTon" text in the Inspector
        EditorGUILayout.LabelField("SingleTon", EditorStyles.boldLabel);
        // Draw the default inspector (displays the default fields of MyComponent)
        DrawDefaultInspector();
    }
}

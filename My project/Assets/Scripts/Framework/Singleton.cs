using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // The single instance of the class
    private static T _instance;

    // Property to get the instance
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // Look for an existing instance of this class in the scene
                _instance = FindObjectOfType<T>();

                // If no instance exists, create a new GameObject and attach this component
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    // // Optional: Make sure the instance doesn't get destroyed when loading new scenes
    // protected virtual void Awake()
    // {
    //     if (_instance == null)
    //     {
    //         _instance = this as T;
    //         DontDestroyOnLoad(gameObject); // Keep this object persistent across scenes
    //     }
    //     else
    //     {
    //         Destroy(gameObject); // Ensure only one instance exists
    //     }
    // }
}

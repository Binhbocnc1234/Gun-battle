using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Chest, container, positionContainer;

    public Timer diffTime = new Timer(8f);
    void Start()
    {
        Chest.gameObject.SetActive(false);
        foreach(Transform child in positionContainer){
            child.gameObject.SetActive(false);
        }
        diffTime.curTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (diffTime.Count()){
            Vector3 pos = positionContainer.GetChild(Random.Range(0, positionContainer.childCount)).position;
            Transform chestObj = Instantiate(Chest, pos, Quaternion.Euler(0,0,0));
            chestObj.gameObject.SetActive(true);
            Chest chest = chestObj.GetComponent<Chest>();
            Transform itemList = ObjectHolder.instance.itemList;
            chest.item = itemList.GetChild(Random.Range(0, itemList.childCount)).GetComponent<Item>();

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction{Left, Right, Up, Down}
public class LoopingBackground : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Direction direction;
    public RectTransform image; //Image's size is the same as Rect's size
    Canvas canvas;
    [HideInInspector] int imageCount;
    void Start()
    {

        canvas = MMObjectHolder.instance.canvas_main;
        imageCount = (int)(canvas.GetComponent<RectTransform>().sizeDelta.x/image.sizeDelta.x) + 2;
        Debug.Log(imageCount);
        for(int i = 1; i < imageCount;++i){
            // Debug.Log("Enter");
            RectTransform other = Instantiate(image, transform).GetComponent<RectTransform>();
            Vector2 temp = other.anchoredPosition;
            temp.x += -image.sizeDelta.x*i + 2;
            // Debug.Log(temp.x);
            other.anchoredPosition = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float jumpDist = speed*Time.deltaTime;
        float minPos = 0f;
        
        foreach(RectTransform child in transform){
            if (direction == Direction.Right){
                child.transform.Translate(new Vector3(1, 0, 0)*jumpDist);
            }
            else if (direction == Direction.Left){
                child.transform.Translate(new Vector3(-1, 0, 0)*jumpDist);
            }
            else if(direction == Direction.Up){
                child.transform.Translate(new Vector3(0, 1, 0)*jumpDist);
            }
            else if (direction == Direction.Down){
                child.transform.Translate(new Vector3(0, -1, 0)*jumpDist);
            }
            minPos = Mathf.Min(minPos, child.anchoredPosition.x);
            if (child.anchoredPosition.x > image.GetComponent<RectTransform>().sizeDelta.x){
                Vector2 temp = child.anchoredPosition;
                temp.x = minPos - image.sizeDelta.x + 2;
                child.anchoredPosition = temp;
            }
        }
        

    }
}

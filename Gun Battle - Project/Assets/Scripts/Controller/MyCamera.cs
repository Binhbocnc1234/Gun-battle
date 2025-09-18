using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public Player pl_1, pl_2;
    public float padding;
    private float original_z;
    private SmoothCamera smoothCam;
    void Start()
    {
        original_z = transform.position.z;
        smoothCam = GetComponent<SmoothCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos = (pl_1.transform.position + pl_2.transform.position)/2.0f;
        pos.z = original_z;
        smoothCam.nextPosition = pos;
        smoothCam.targetSize = CalculateTargetSize();
        // if (Mathf.Abs(smoothCam.targetSize - Camera.main.orthographicSize) <= 0.2){
        //     if (IsPlayersInCamera(5) == false ){
        //         smoothCam.targetSize += 0.05f;
        //     }
        //     else{
        //         smoothCam.targetSize -= 0.05f;
        //     }
        // }
        
    }
    bool IsPlayersInCamera(float offset){
        return (IsPositionInCamera2D(pl_1.transform.position, Camera.main, offset) &&
        IsPositionInCamera2D(pl_2.transform.position, Camera.main, offset));
    }
    bool IsPositionInCamera2D(Vector3 position, Camera camera, float offset = 0f){
        // Get the orthographic size of the camera
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        // Get the camera's world position
        Vector3 cameraPos = camera.transform.position;
        // Calculate the camera's bounds in world coordinates
        float leftBound = cameraPos.x - cameraWidth / 2f;
        float rightBound = cameraPos.x + cameraWidth / 2f;
        float bottomBound = cameraPos.y - cameraHeight / 2f;
        float topBound = cameraPos.y + cameraHeight / 2f;
        // Check if the position is within the camera's bounds
        return (position.x - offset >= leftBound && position.x + offset <= rightBound &&
                position.y - offset >= bottomBound && position.y + offset<= topBound);
    }
    float CalculateTargetSize()
    {
        // Calculate the distance between the two players in both x and y axes
        float distanceX = Mathf.Abs(pl_1.transform.position.x - pl_2.transform.position.x);
        float distanceY = Mathf.Abs(pl_1.transform.position.y - pl_2.transform.position.y);

        // Add padding to the distances
        distanceX += padding;
        distanceY += padding;

        // Calculate the orthographic size needed to fit the players vertically (y-axis)
        float requiredSizeY = distanceY / 2f;

        // Calculate the orthographic size needed to fit the players horizontally (x-axis) by taking the aspect ratio into account
        float cameraAspect = Camera.main.aspect;
        float requiredSizeX = distanceX / (2f * cameraAspect);

        // The target size should be the larger of the required sizes
        return Mathf.Max(requiredSizeY, requiredSizeX);
    }

}

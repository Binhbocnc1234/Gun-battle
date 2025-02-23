using UnityEngine;

public class HarmonicOscillation : MonoBehaviour
{
    public float amplitude = 0.1f; // The maximum displacement from the starting position
    public float frequency = 1f; // Oscillation speed in cycles per second (Hz)
    private float startY;        // The initial Y position of the object
    Vector3 diff = new Vector3(0, 0, 0);
    void Start()
    {
        // Record the starting Y position
        startY = transform.position.y;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = startY + amplitude * Mathf.Sin(2 * Mathf.PI * frequency * Time.time);

        // Apply the new position to the object
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    void LateUpdate(){

    }
}

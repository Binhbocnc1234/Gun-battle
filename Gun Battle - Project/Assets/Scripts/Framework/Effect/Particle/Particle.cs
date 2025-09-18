using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Particle : MonoBehaviour
{
    public float weight = 1f; // Trọng lượng
    public float existingTime = 1f; // Thời gian tồn tại
    public float fadeOutTime = 0.5f; // Thời gian fade out

    private Timer lifeTimer;
    private Timer fadeTimer;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private bool isFading = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = weight;
        lifeTimer = new Timer(existingTime);
        fadeTimer = new Timer(fadeOutTime);
    }

    void Start()
    {
        lifeTimer.Reset();
        fadeTimer.curTime = 0;
    }

    void Update()
    {
        if (!isFading)
        {
            if (lifeTimer.Count(false))
            {
                isFading = true;
                fadeTimer.Reset();
            }
        }
        else
        {
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer.curTime / fadeOutTime);
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;

            if (fadeTimer.Count(false))
            {
                Destroy(gameObject);
            }
        }
    }
}
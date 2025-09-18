
using System.Collections.Generic;
using UnityEngine;

public enum ParticleLocationType
{
    Default,
    Explosion,
    BloodSplash,
    Jump,
    Fire,
    WaterSplash,
    // Thêm các loại khác nếu cần
}

public class ParticleLocation : MonoBehaviour
{
    public ParticleLocationType locationType = ParticleLocationType.Default;
    public Particle particlePrefab;
    public float force = 5f;
    public float spawnRate = 10f; // particles per second
    public float existingTime = 1f;
    public List<Particle> particles = new List<Particle>();
    [HideInInspector] public Timer spawnTimer;

    void Awake()
    {
        spawnTimer = new Timer(1f / spawnRate);
    }

    public void SetAttribute(ParticleLocationSO so)
    {
        locationType = so.locationType;
        particlePrefab = so.particlePrefab;
        force = so.force;
        spawnRate = so.spawnRate;
        existingTime = so.existingTime;
        spawnTimer = new Timer(1f / so.spawnRate);
    }
    private Timer existingTimer;
    void Start()
    {
        existingTimer = new Timer(existingTime);

    }
    void Update()
    {
        if (existingTimer.Count())
        {
            Destroy(this.gameObject);
        }
    }
}

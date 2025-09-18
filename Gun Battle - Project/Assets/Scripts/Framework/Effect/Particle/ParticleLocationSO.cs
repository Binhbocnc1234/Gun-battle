using UnityEngine;

[CreateAssetMenu(fileName = "NewParticleSO", menuName = "ScriptableObject/Particle")]
public class ParticleLocationSO : ScriptableObject
{
    public ParticleLocationType locationType = ParticleLocationType.Default;
    public Particle particlePrefab;
    public float force = 5f;
    public float spawnRate = 10f;
    public float existingTime = 1f;
}

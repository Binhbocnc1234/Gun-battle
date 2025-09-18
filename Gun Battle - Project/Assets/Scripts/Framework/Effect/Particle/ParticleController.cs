using System.Collections.Generic;
using UnityEngine;

public class ParticleController : Singleton<ParticleController>
{
    // ScriptableObject lưu thông số cho từng loại location
    public List<ParticleLocationSO> locationSOs = new List<ParticleLocationSO>();

    // Tự động load toàn bộ LocationSO từ Resources/ScriptableObject/Particle
    public void LoadAllLocationSO()
    {
        locationSOs.Clear();
        ParticleLocationSO[] loaded = Resources.LoadAll<ParticleLocationSO>("ScriptableObject/Particle");
        locationSOs.AddRange(loaded);
    }

    void Awake()
    {
        LoadAllLocationSO();
    }
    void Start()
    {
        
    }
    void Update()
    {
        foreach (Transform l in this.transform)
        {
            ParticleLocation loc = l.GetComponent<ParticleLocation>();
            if (loc.spawnTimer.Count())
            {
                SpawnParticle(loc);
            }
        }
    }

    // Tạo location tại vị trí cố định
    public void AddLocation(ParticleLocationType type, Vector3 pos)
    {
        ParticleLocationSO so = locationSOs.Find(x => x.locationType == type);
        if (so == null)
        {
            Debug.LogError($"Không tìm thấy ParticleLocationSO cho loại {type}");
            return;
        }
        GameObject go = new GameObject($"ParticleLocation_{type}");
        go.transform.position = pos;
        ParticleLocation loc = go.AddComponent<ParticleLocation>();
        loc.SetAttribute(so);
        go.transform.SetParent(this.transform);
    }

    // Tạo location gắn với target (ví dụ máu chảy từ địch)
    public void AddLocation(ParticleLocationType type, Transform target)
    {
        ParticleLocationSO so = locationSOs.Find(x => x.locationType == type);
        if (so == null)
        {
            Debug.LogError($"Không tìm thấy ParticleLocationSO cho loại {type}");
            return;
        }
        GameObject go = new GameObject($"ParticleLocation_{type}_Target");
        go.transform.SetParent(target);
        go.transform.localPosition = Vector3.zero;
        ParticleLocation loc = go.AddComponent<ParticleLocation>();
        loc.SetAttribute(so);
        go.transform.SetParent(this.transform);
    }
    void SpawnParticle(ParticleLocation loc)
    {
        Particle p = Instantiate(loc.particlePrefab, loc.transform.position, Quaternion.identity, loc.transform);
        loc.particles.Add(p);

        // Apply force
        Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 dir = Random.insideUnitCircle.normalized;
            rb.AddForce(dir * loc.force, ForceMode2D.Impulse);
        }
    }
}
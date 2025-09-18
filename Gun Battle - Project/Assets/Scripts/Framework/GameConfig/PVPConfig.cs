using UnityEngine;

[CreateAssetMenu(fileName = "NewPVPConfig", menuName = "ScriptableObject/PVPConfig")]
public class PVPConfig : ScriptableObject
{
    public string defaultGun = "Colt 45";
    public int objective = 5;
    public float gravity = 9.8f;
    public static PVPConfig GetPVPConfig()
    {
        return Resources.Load<PVPConfig>("ScriptableObject/GameConfig/PVPConfig");
    }
}
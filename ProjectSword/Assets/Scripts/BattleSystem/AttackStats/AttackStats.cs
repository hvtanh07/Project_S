using UnityEngine;
[System.Serializable]

public enum AttackType
{
    Dash,
    MultipleDash,
    Magic,
    Projectile,
    Suicide,
    Touch
}
public abstract class AttackStats : MonoBehaviour
{
    public AttackType type;
    public bool unlocked = false;
}

using UnityEngine;
[System.Serializable]

public enum AttackType
{
    Dash,
    MultipleDash,
    Lightning,
    Rock,
    Circle,
    Projectile,
    Suicide,
    Touch
}
public abstract class AttackStats : MonoBehaviour
{
    public float distanceToAttack;
    public float timeBetweenAtack;
    public int damage;
    public AttackType type;
    public bool unlocked = false;
}

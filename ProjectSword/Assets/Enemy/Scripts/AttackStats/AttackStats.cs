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
    public int health;
    public float speed;
    public float distanceToAttack;
    public float timeBetweenAtack;
    public int damage;
    public AttackType type;
    public bool unlocked = false;
    //public RuntimeAnimatorController anim;
}

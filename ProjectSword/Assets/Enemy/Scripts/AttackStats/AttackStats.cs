using UnityEngine;
[System.Serializable]

public enum AttackType
{
    Doggo,
    Hound,
    LightningHare,
    RockHare,
    SpearHare,
    Porcupine,
    Baboom,
    Mousey
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
}

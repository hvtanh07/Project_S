using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public float distanceToAttack;
    public float timeBetweenAtack;
    public int damage;
    public abstract void Attacking(Vector3 target);
    //public abstract void DrawAttackPatern(Vector3 target);
}

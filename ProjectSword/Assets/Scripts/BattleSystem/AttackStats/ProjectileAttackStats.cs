using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackStats : AttackStats
{
    public GameObject objBullet;
    public float Force;
    
    [Range(0, 3)]
    public int numOfSideProjectiles;
    public int spreadAngle;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcupineAttackStats : AttackStats
{
    public GameObject objBullet;
    public float Force;
    
    [Range(0, 3)]
    public int numOfSideProjectiles;
    public float spreadAngle;
}

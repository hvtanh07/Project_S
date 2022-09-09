using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttackStats : AttackStats
{
    public GameObject rock;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;   
    public LayerMask playerMask;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHareAttackStats : AttackStats
{
    public GameObject rock;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;   
    public LayerMask playerMask;
}

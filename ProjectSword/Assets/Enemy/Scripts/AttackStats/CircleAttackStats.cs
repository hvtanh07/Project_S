using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttackStats : AttackStats
{
    public GameObject circle;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;
    public LayerMask playerMask;
}

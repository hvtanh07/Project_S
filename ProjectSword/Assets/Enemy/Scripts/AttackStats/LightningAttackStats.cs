using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttackStats : AttackStats
{
    public GameObject lightning;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;   
    public LayerMask playerMask;
}

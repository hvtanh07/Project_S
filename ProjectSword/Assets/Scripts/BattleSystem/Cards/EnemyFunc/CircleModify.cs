using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleModify : CardFunc
{
    [SerializeField] CircleAttackStats stats;
    public float additionalSpotSpreadRange;
    public int additionalNumberOfAttackPoint;
    public override void GiveAdditionalStats()
    {
        stats.spotSpreadRange += additionalSpotSpreadRange;
        stats.numberOfAttackPoint += additionalNumberOfAttackPoint;
    }
}

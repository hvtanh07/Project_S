using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningHareModify : CardFunc
{
    [SerializeField] LightningHareAttackStats stats;
    public float additionalSpotSpreadRange;
    public int additionalNumberOfAttackPoint;
    public override void GiveAdditionalStats()
    {
        stats.spotSpreadRange += additionalSpotSpreadRange;
        stats.numberOfAttackPoint += additionalNumberOfAttackPoint;
    }
}

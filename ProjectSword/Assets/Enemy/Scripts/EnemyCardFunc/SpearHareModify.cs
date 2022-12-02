using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHareModify : CardFunc
{
    [SerializeField] SpearHareAttackStats stats;
    public int additionalDamage;
    public float additionalSpotSpreadRange;

    public int additionalNumberOfAttackPoint;
    public override void GiveAdditionalStats()
    {
        stats.damage += additionalDamage;
        stats.spotSpreadRange += additionalSpotSpreadRange;
        stats.numberOfAttackPoint += additionalNumberOfAttackPoint;
    }
}

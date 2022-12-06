using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningHareModify : CardFunc
{
    [SerializeField] LightningHareAttackStats stats;
    public float additionalPercentageHealth;
    public float additionalPercentageDamage;
    public float additionalSpotSpreadRange;
    public int additionalNumberOfAttackPoint;
    public override void GiveAdditionalStats()
    {
        stats.health += (int)Mathf.Round(stats.health*additionalPercentageHealth);
        stats.damage += (int)Mathf.Round(stats.damage*additionalPercentageDamage);
        stats.spotSpreadRange += additionalSpotSpreadRange;
        stats.numberOfAttackPoint += additionalNumberOfAttackPoint;
    }
}

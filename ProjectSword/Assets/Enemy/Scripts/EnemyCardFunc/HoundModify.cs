using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoundModify : CardFunc
{
    [SerializeField] HoundAttackStats stats;
    public float additionalPercentageHealth;
    public float additionalPercentageDamage;
    public float additionalPercentageTimeBetweenAtack;

    public override void GiveAdditionalStats()
    {
        stats.health += (int)Mathf.Round(stats.health*additionalPercentageHealth);
        stats.damage += (int)Mathf.Round(stats.damage*additionalPercentageDamage);
        stats.timeBetweenAtack += (int)Mathf.Round(stats.timeBetweenAtack*additionalPercentageTimeBetweenAtack);
    }
}

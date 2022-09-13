using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModify : CardFunc
{
    
    public int additionalDamage;
    public float additionalDashDistance;
    public float additionalDashSpeed;
    public float additionalMaxDashs;
    public float additionalDashHealTime;
    [SerializeField] PlayerAttack stats;
    public override void GiveAdditionalStats()
    {
        stats.damage += additionalDamage;
        stats.dashDistance += additionalDashDistance;
        stats.dashSpeed += additionalDashSpeed;
        stats.maxDashs += additionalMaxDashs;
        stats.dashHealTime -= additionalDashHealTime;
    }
}

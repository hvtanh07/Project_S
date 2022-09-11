using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockEnemy : EnemyCardFunc
{
    [SerializeField] AttackStats stats;
    public override void GiveAdditionalStats()
    {
        if (!stats.unlocked)
        {
            stats.unlocked = true;
        }
    }
}

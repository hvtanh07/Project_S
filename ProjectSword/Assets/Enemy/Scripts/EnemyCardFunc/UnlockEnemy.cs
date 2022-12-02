using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockEnemy : CardFunc
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

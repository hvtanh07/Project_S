using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDefend : CardFunc
{
    [SerializeField] EnemyCreator stats;
    public override void GiveAdditionalStats()
    {
        if (!stats.DefendEnable)
        {
            stats.enabled = true;
        }
    }
}

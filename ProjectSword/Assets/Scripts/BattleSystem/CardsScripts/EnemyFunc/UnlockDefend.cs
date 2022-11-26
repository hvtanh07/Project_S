using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDefend : CardFunc
{
    [SerializeField] DefendStats stats;
    public override void GiveAdditionalStats()
    {
        if (!stats.unlocked)
        {
            stats.enabled = true;
        }
    }
}

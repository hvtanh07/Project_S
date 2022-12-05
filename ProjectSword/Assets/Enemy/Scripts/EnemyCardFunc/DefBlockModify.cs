using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefBlockModify : CardFunc
{
    [SerializeField] BlockStats stats;
    public float additionalBlockHealTime;
    public override void GiveAdditionalStats()
    {
        stats.blockHealTime += additionalBlockHealTime;
    }
}

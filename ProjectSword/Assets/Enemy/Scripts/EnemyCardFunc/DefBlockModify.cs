using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefBlockModify : CardFunc
{
    [SerializeField] BlockStats stats;
    public float additionalPercentageBlockHealTime;
    public override void GiveAdditionalStats()
    {
        stats.blockHealTime += (int)Mathf.Round(stats.blockHealTime*additionalPercentageBlockHealTime);
    }
}

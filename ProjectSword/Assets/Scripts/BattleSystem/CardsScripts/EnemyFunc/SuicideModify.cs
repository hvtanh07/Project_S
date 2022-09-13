using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideModify : CardFunc
{
    [SerializeField] SuicideBombStats stats;
    public float additionalRadius;
    public override void GiveAdditionalStats()
    {
        stats.radius += additionalRadius;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaboomModify : CardFunc
{
    [SerializeField] BaboomAttackStats stats;
    public float additionalRadius;
    public override void GiveAdditionalStats()
    {
        stats.radius += additionalRadius;
    }
}

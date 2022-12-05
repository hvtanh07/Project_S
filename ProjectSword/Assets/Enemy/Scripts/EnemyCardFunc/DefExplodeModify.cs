using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefExplodeModify : CardFunc
{
    [SerializeField] ExplodeStats stats;
    public int additionalRadius;
    public int additionalDamage;
    public override void GiveAdditionalStats()
    {
        stats.radius += additionalRadius;
        stats.damage += additionalDamage;
    }
}

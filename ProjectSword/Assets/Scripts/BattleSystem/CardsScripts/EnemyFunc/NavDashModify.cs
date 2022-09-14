using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDashModify : CardFunc
{
    [SerializeField] NavDashBackupStats stats;
    public float additionalDistanceToEvade;
    public float additionalDashCoolDown;
    public override void GiveAdditionalStats()
    {
        stats.distanceToEvade += additionalDistanceToEvade;
        stats.dashCoolDown += additionalDashCoolDown;
    }
}

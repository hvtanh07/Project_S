using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoundModify : CardFunc
{
    [SerializeField] HoundAttackStats stats;
    public float AdditionalDashSpeed;
    public Vector3 dashPoint;

    public override void GiveAdditionalStats()
    {
        stats.dashSpeed += AdditionalDashSpeed;
        if(dashPoint != Vector3.zero){
            stats.patern.Add(dashPoint);
        }
    }
}
using UnityEngine;

public class DashModify : CardFunc
{
    [SerializeField] DashAttackStats stats;
    public float AdditionalDashDistance;
    public float AdditionalDashSpeed;

    public override void GiveAdditionalStats(){
        stats.dashDistance += AdditionalDashDistance;
        stats.dashSpeed += AdditionalDashSpeed;
    }
}

using UnityEngine;

public class DashModify : EnemyCardFunc
{
    [SerializeField] DashAttackStats stats;
    public float AdditionalDashDistance;
    public float AdditionalDashSpeed;

    public override void GiveAdditionalStats(){
        stats.dashDistance += AdditionalDashDistance;
        stats.dashSpeed += AdditionalDashSpeed;
    }
}

using UnityEngine;

public class DoggoModify : CardFunc
{
    [SerializeField] DoggoAttackStats stats;
    public float AdditionalDashDistance;
    public float AdditionalDashSpeed;

    public override void GiveAdditionalStats(){
        stats.dashDistance += AdditionalDashDistance;
        stats.dashSpeed += AdditionalDashSpeed;
    }
}

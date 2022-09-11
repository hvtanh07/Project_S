
using UnityEngine;

public class ArrrowModify : BaseEnemyModify
{
    [SerializeField] ProjectileAttackStats stats;
    public float additionalForce;
    public int additionalNumOfSideProjectiles;
    public int additionalSpreadAngle;
    public override void GiveAdditionalStats(){
        stats.Force += additionalForce;
        stats.numOfSideProjectiles += additionalNumOfSideProjectiles;
        stats.spreadAngle += additionalSpreadAngle;
    }
}

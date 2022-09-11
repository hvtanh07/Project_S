
using UnityEngine;

public class BaseEnemyModify : EnemyCardFunc
{
    [SerializeField] AttackStats enemytStats;
    public int additionalHealth;
    public float additionalSpeed;
    public int additionalDamage;

    public override void GiveAdditionalStats(){
        enemytStats.damage += additionalDamage;
        enemytStats.speed += additionalSpeed;
        enemytStats.health += additionalHealth;
    }
}

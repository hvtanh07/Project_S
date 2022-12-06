using UnityEngine;

public class DoggoModify : CardFunc
{
    [SerializeField] DoggoAttackStats stats;
    public float additionalPercentageHealth;
    public float additionalPercentageDamage;
    public float additionalPercentageTimeBetweenAtack;

    public override void GiveAdditionalStats(){
        stats.health += (int)Mathf.Round(stats.health*additionalPercentageHealth);
        stats.damage += (int)Mathf.Round(stats.damage*additionalPercentageDamage);
        stats.timeBetweenAtack += (int)Mathf.Round(stats.timeBetweenAtack*additionalPercentageTimeBetweenAtack);
    }
}

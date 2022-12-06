
using UnityEngine;

public class PorcupineModify : CardFunc
{
    [SerializeField] PorcupineAttackStats stats;
    public float additionalPercentageHealth;
    public float additionalPercentageDamage;
    public int additionalNumOfSideProjectiles;
    public float additionalPercentageSpreadAngle;
    public override void GiveAdditionalStats()
    {
        stats.health += (int)Mathf.Round(stats.health * additionalPercentageHealth);
        stats.damage += (int)Mathf.Round(stats.damage * additionalPercentageDamage);
        stats.numOfSideProjectiles += additionalNumOfSideProjectiles;
        stats.spreadAngle += (int)Mathf.Round(stats.spreadAngle * additionalPercentageSpreadAngle);
    }
}

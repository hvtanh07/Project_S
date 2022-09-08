using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public GameObject baseEnemy;

    int RandAttack;
    [Space]
    [Header("Attack System")]
    public int currentEnemyDamage;
    [SerializeField] private List<AttackStats> allAttackType;
    [SerializeField] private List<AttackStats> unlockedAttackType;

    [Space]
    [Header("Nav System")]
    [SerializeField] private List<NavStats> allNavType;
    [SerializeField] private List<NavStats> unlockedNavType;
    //[SerializeField] private 

    [Space]
    [Header("Defend System")]
    public bool DefendEnable;
    [SerializeField] private DefendStats DefendStats;
    private void Start() {
        UpdateUnlockedTypes();
    }

    public void UpdateUnlockedTypes(){
        unlockedAttackType.Clear();
        foreach(AttackStats AttackType in allAttackType){
            if(AttackType.unlocked){
                unlockedAttackType.Add(AttackType);
            }
        }
        unlockedNavType.Clear();
        foreach(NavStats NavType in allNavType){
            if(NavType.unlocked){
                unlockedNavType.Add(NavType);
            }
        }
    }

    private void AssignAttack(GameObject enemy)
    {
        RandAttack = Random.Range(0, unlockedAttackType.Count);
        switch (unlockedAttackType[RandAttack].type)
        {
            case AttackType.Dash:
                {
                    DashAttack attack = enemy.AddComponent<DashAttack>();
                    DashAttackStats stats = unlockedAttackType[RandAttack] as DashAttackStats;
                    attack.damage = currentEnemyDamage;
                    attack.dashDistance = stats.dashDistance;
                    attack.dashSpeed = stats.dashSpeed;
                    attack.wallmask = stats.wallmask;
                    break;
                }

            case AttackType.MultipleDash:
                {
                    MultipleDashAttack attack = enemy.AddComponent<MultipleDashAttack>();
                    MultipleDashAttackStats stats = unlockedAttackType[RandAttack] as MultipleDashAttackStats;
                    attack.damage = currentEnemyDamage;
                    attack.dashSpeed = stats.dashSpeed;
                    attack.patern = stats.patern;
                    break;
                }
            case AttackType.Magic:
                {
                    MagicAttack attack = enemy.AddComponent<MagicAttack>();
                    MagicAttackStats stats = unlockedAttackType[RandAttack] as MagicAttackStats;
                    attack.damage = currentEnemyDamage;
                    attack.lightning = stats.lightning;//replace with current type of magic function()
                    attack.spotWidth = stats.spotWidth;
                    attack.spotSpreadRange = stats.spotSpreadRange;
                    attack.numberOfAttackPoint = stats.numberOfAttackPoint;
                    attack.typeOfMagic = stats.typeOfMagic;
                    attack.playerMask = stats.playerMask;
                    break;
                }
            case AttackType.Projectile:
                {
                    ProjectileAttack attack = enemy.AddComponent<ProjectileAttack>();
                    ProjectileAttackStats stats = unlockedAttackType[RandAttack] as ProjectileAttackStats;
                    attack.damage = currentEnemyDamage;
                    attack.objBullet = stats.objBullet;
                    attack.Force = stats.Force;
                    attack.numOfSideProjectiles = stats.numOfSideProjectiles;
                    attack.spreadAngle = stats.spreadAngle;
                    break;
                }
            case AttackType.Suicide:
                {
                    SuicideBomb attack = enemy.AddComponent<SuicideBomb>();
                    SuicideBombStats stats = unlockedAttackType[RandAttack] as SuicideBombStats;
                    attack.damage = currentEnemyDamage;
                    attack.explosion = stats.explosion;
                    break;
                }
            case AttackType.Touch:
                {
                    TouchAttack attack = enemy.AddComponent<TouchAttack>();
                    TouchAttackStats stats = unlockedAttackType[RandAttack] as TouchAttackStats;
                    attack.damage = currentEnemyDamage;
                    break;
                }
        }
    }
    private void AssignNav(GameObject enemy)
    {

        if (unlockedAttackType[RandAttack].type == AttackType.Suicide || unlockedAttackType[RandAttack].type == AttackType.Touch)
        {
            NavRunToTarget nav = enemy.AddComponent<NavRunToTarget>();
        }
        else
        {
            int navType = Random.Range(0, unlockedNavType.Count);
            switch (unlockedNavType[navType].type)
            {
                case NavType.Dash:
                    {
                        NavDashBackup nav = enemy.AddComponent<NavDashBackup>();
                        NavDashBackupStats stats = unlockedNavType[navType] as NavDashBackupStats;
                        nav.dashCoolDown = stats.dashCoolDown;
                        nav.wallMask = stats.wallMask;
                        break;
                    }
                case NavType.KeepDistance:
                    {
                        NavKeepDistance nav = enemy.AddComponent<NavKeepDistance>();
                        break;
                    }
                case NavType.Walk:
                    {
                        NavRunToTarget nav = enemy.AddComponent<NavRunToTarget>();
                        break;
                    }

            }
        }

    }

    public GameObject GetEnemy()
    {
        GameObject enemy = Instantiate(baseEnemy);
        AssignAttack(enemy);
        AssignNav(enemy);
        if (unlockedAttackType[RandAttack].type != AttackType.Suicide && DefendEnable && Random.value > 0.5f)
        {
            Shield shield = enemy.AddComponent<Shield>();
            shield.blockHealTime = DefendStats.blockHealTime;
            shield.maximumBlockHealth = DefendStats.maximumBlockHealth;
            shield.maximumBlockHealth = DefendStats.maximumBlockHealth;
        }
        return enemy;
    }
}

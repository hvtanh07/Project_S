using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{

    public static BattleSystem instance { get; private set; }
    private int RandAttack;
    public GameObject baseEnemy;
    [Space]
    [Header("Attack System")]
    public int currentEnemyDamage;
    [SerializeField] private List<AttackStats> allAttackType;
    [SerializeField] private List<AttackStats> unlockedAttackType;

    [Space]
    [Header("Nav System")]
    [SerializeField] private List<NavStats> allNavType;
    [SerializeField] private List<NavStats> unlockedNavType;

    [Space]
    [Header("Def System")]
    [SerializeField] private List<DefendStats> allDefType;
    [SerializeField] private List<DefendStats> unlockedDefType;
    //[SerializeField] private 

    private void Start()
    {
        UpdateUnlockedTypes();
    }

    public void UpdateUnlockedTypes()
    {
        unlockedAttackType = allAttackType.FindAll(x => x.unlocked && !unlockedAttackType.Contains(x));
        unlockedNavType = allNavType.FindAll(x => x.unlocked && !unlockedNavType.Contains(x));
        unlockedDefType = allDefType.FindAll(x => x.unlocked && !unlockedDefType.Contains(x));
    }

    private void AssignAttack(GameObject enemy)
    {
        RandAttack = Random.Range(0, unlockedAttackType.Count);
        //enemy.GetComponent<Ronin>().animR.runtimeAnimatorController  =  unlockedAttackType[RandAttack].anim;
        switch (unlockedAttackType[RandAttack].type)
        {
            case AttackType.Dash:
                {
                    DashAttack attack = enemy.AddComponent<DashAttack>();
                    DashAttackStats stats = unlockedAttackType[RandAttack] as DashAttackStats;
                    enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("EnemyAnimController/RoninChase");
                    attack.distanceToAttack = stats.distanceToAttack;
                    attack.timeBetweenAtack = stats.timeBetweenAtack;
                    attack.damage = stats.damage;
                    attack.dashDistance = stats.dashDistance;
                    attack.dashSpeed = stats.dashSpeed;
                    attack.wallmask = stats.wallmask;
                    Instantiate(stats.trail, enemy.transform).transform.parent = enemy.transform;
                    attack.GetTrail();
                    break;
                }

            case AttackType.MultipleDash:
                {
                    MultipleDashAttack attack = enemy.AddComponent<MultipleDashAttack>();
                    MultipleDashAttackStats stats = unlockedAttackType[RandAttack] as MultipleDashAttackStats;
                    enemy.GetComponent<Ronin>().animR.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("EnemyAnimController/RoninChase");
                    attack.distanceToAttack = stats.distanceToAttack;
                    attack.timeBetweenAtack = stats.timeBetweenAtack;
                    attack.damage = stats.damage;
                    attack.dashSpeed = stats.dashSpeed;
                    attack.patern = stats.patern;
                    Instantiate(stats.trail, enemy.transform).transform.parent = enemy.transform;
                    attack.GetTrail();
                    break;
                }
            case AttackType.Lightning:
                {
                    LighningAttack attack = enemy.AddComponent<LighningAttack>();
                    LightningAttackStats stats = unlockedAttackType[RandAttack] as LightningAttackStats;
                    enemy.GetComponent<Ronin>().animR.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("EnemyAnimController/RoninChase");
                    attack.distanceToAttack = stats.distanceToAttack;
                    attack.timeBetweenAtack = stats.timeBetweenAtack;
                    attack.damage = stats.damage;
                    attack.lightning = stats.lightning;
                    attack.spotWidth = stats.spotWidth;
                    attack.spotSpreadRange = stats.spotSpreadRange;
                    attack.numberOfAttackPoint = stats.numberOfAttackPoint;
                    attack.playerMask = stats.playerMask;
                    break;
                }
            case AttackType.Rock:
                {
                    RockAttack attack = enemy.AddComponent<RockAttack>();
                    RockAttackStats stats = unlockedAttackType[RandAttack] as RockAttackStats;
                    enemy.GetComponent<Ronin>().animR.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("EnemyAnimController/RoninChase");
                    attack.distanceToAttack = stats.distanceToAttack;
                    attack.timeBetweenAtack = stats.timeBetweenAtack;
                    attack.damage = stats.damage;
                    attack.spell = stats.rock;
                    attack.spotWidth = stats.spotWidth;
                    attack.spotSpreadRange = stats.spotSpreadRange;
                    attack.numberOfAttackPoint = stats.numberOfAttackPoint;
                    attack.playerMask = stats.playerMask;
                    break;
                }
            case AttackType.Circle:
                {
                    CircleAttack attack = enemy.AddComponent<CircleAttack>();
                    CircleAttackStats stats = unlockedAttackType[RandAttack] as CircleAttackStats;
                    enemy.GetComponent<Ronin>().animR.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("EnemyAnimController/RoninChase");
                    attack.distanceToAttack = stats.distanceToAttack;
                    attack.timeBetweenAtack = stats.timeBetweenAtack;
                    attack.damage = stats.damage;
                    attack.spell = stats.circle;
                    attack.spotWidth = stats.spotWidth;
                    attack.spotSpreadRange = stats.spotSpreadRange;
                    attack.numberOfAttackPoint = stats.numberOfAttackPoint;
                    attack.playerMask = stats.playerMask;
                    break;
                }
            case AttackType.Projectile:
                {
                    ProjectileAttack attack = enemy.AddComponent<ProjectileAttack>();
                    ProjectileAttackStats stats = unlockedAttackType[RandAttack] as ProjectileAttackStats;
                    enemy.GetComponent<Ronin>().animR.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("EnemyAnimController/RoninChase");
                    attack.distanceToAttack = stats.distanceToAttack;
                    attack.timeBetweenAtack = stats.timeBetweenAtack;
                    attack.damage = stats.damage;
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
                    //enemy.GetComponent<Ronin>().animR.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("EnemyAnimController/RoninChase");
                    attack.distanceToAttack = stats.distanceToAttack;
                    attack.timeBetweenAtack = stats.timeBetweenAtack;
                    attack.damage = stats.damage;
                    attack.explosion = stats.explosion;
                    break;
                }
            case AttackType.Touch:
                {
                    TouchAttack attack = enemy.AddComponent<TouchAttack>();
                    TouchAttackStats stats = unlockedAttackType[RandAttack] as TouchAttackStats;
                    enemy.GetComponent<Ronin>().animR.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("EnemyAnimController/RoninChase");
                    attack.distanceToAttack = stats.distanceToAttack;
                    attack.timeBetweenAtack = stats.timeBetweenAtack;
                    attack.damage = stats.damage;
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
                        nav.SpeedMultiplier = stats.SpeedMultiplier;
                        nav.dashCoolDown = stats.dashCoolDown;
                        nav.distanceToEvade = stats.distanceToEvade;
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
    private void AssignDef(GameObject enemy)
    {
        if (unlockedDefType.Count > 0 && (unlockedAttackType[RandAttack].type == AttackType.Lightning || 
        unlockedAttackType[RandAttack].type == AttackType.Rock || 
        unlockedAttackType[RandAttack].type == AttackType.Touch || 
        unlockedAttackType[RandAttack].type == AttackType.Circle))
        {
            int defType = Random.Range(0, unlockedDefType.Count);
            switch (unlockedDefType[defType].type)
            {
                case DefendType.Block:
                    {
                        Shield shield = enemy.AddComponent<Shield>();
                        BlockStats stats = unlockedDefType[defType] as BlockStats;
                        shield.blockHealTime = stats.blockHealTime;
                        shield.maximumBlockHealth = stats.maximumBlockHealth;
                        shield.healAmount = stats.healAmount;
                        break;
                    }
                case DefendType.Decoy:
                    {
                        Decoy decoy = enemy.AddComponent<Decoy>();
                        DecoyStats stats = unlockedDefType[defType] as DecoyStats;
                        decoy.decoyObj = stats.decoyObj;
                        break;
                    }
            }
        }
        else if(unlockedAttackType[RandAttack].type == AttackType.Suicide){
            ExplodeOnDeath explosion = enemy.AddComponent<ExplodeOnDeath>();
            ExplodeStats stats = unlockedDefType[2] as ExplodeStats;
            explosion.explosion = stats.explosion;
            explosion.maximumDamage = stats.damage;
            explosion.radius = stats.radius;
        }
    }


    public GameObject GetEnemy()
    {
        GameObject enemy = Instantiate(baseEnemy);
        //GameObject enemy = new GameObject("baseEnemy");
        AssignAttack(enemy);
        AssignNav(enemy);
        AssignDef(enemy);

        return enemy;
    }
}

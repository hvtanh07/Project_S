using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{

    public static EnemyCreator instance { get; private set; }
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
    [SerializeField] private BlockStats blockStats;
    [SerializeField] private DecoyStats decoyStats;
    [SerializeField] private ExplodeStats explodeStats;
    //[SerializeField] private 

    private void Start()
    {
        UpdateUnlockedTypes();
    }

    public void UpdateUnlockedTypes()
    {
        foreach (AttackStats attac in allAttackType){
            if (attac.unlocked && !unlockedAttackType.Contains(attac)){
                unlockedAttackType.Add(attac);
            }
        }
        foreach (NavStats nav in allNavType){
            if (nav.unlocked && !unlockedNavType.Contains(nav)){
                unlockedNavType.Add(nav);
            }
        }
        //unlockedAttackType = allAttackType.FindAll(x => x.unlocked && !unlockedAttackType.Contains(x));
        //unlockedNavType = allNavType.FindAll(x => x.unlocked && !unlockedNavType.Contains(x));
    }

    private void AssignAttack(GameObject enemy)
    {
        RandAttack = Random.Range(0, unlockedAttackType.Count);
        switch (unlockedAttackType[RandAttack].type)
        {
            case AttackType.Doggo:
                {
                    DoggoAttack attack = enemy.AddComponent<DoggoAttack>();
                    DoggoAttackStats stats = unlockedAttackType[RandAttack] as DoggoAttackStats;
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

            case AttackType.Hound:
                {
                    HoundAttack attack = enemy.AddComponent<HoundAttack>();
                    HoundAttackStats stats = unlockedAttackType[RandAttack] as HoundAttackStats;
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
            case AttackType.LightningHare:
                {
                    LightningHareAttack attack = enemy.AddComponent<LightningHareAttack>();
                    LightningHareAttackStats stats = unlockedAttackType[RandAttack] as LightningHareAttackStats;
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
            case AttackType.RockHare:
                {
                    RockHareAttack attack = enemy.AddComponent<RockHareAttack>();
                    RockHareAttackStats stats = unlockedAttackType[RandAttack] as RockHareAttackStats;
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
            case AttackType.SpearHare:
                {
                    SpearHareAttack attack = enemy.AddComponent<SpearHareAttack>();
                    SpearHareAttackStats stats = unlockedAttackType[RandAttack] as SpearHareAttackStats;
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
            case AttackType.Porcupine:
                {
                    PorcupineAttack attack = enemy.AddComponent<PorcupineAttack>();
                    PorcupineAttackStats stats = unlockedAttackType[RandAttack] as PorcupineAttackStats;
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
            case AttackType.Baboom:
                {
                    BaboomAttack attack = enemy.AddComponent<BaboomAttack>();
                    BaboomAttackStats stats = unlockedAttackType[RandAttack] as BaboomAttackStats;
                    enemy.GetComponent<Ronin>().animR.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("EnemyAnimController/RoninChase");
                    attack.distanceToAttack = stats.distanceToAttack;
                    attack.timeBetweenAtack = stats.timeBetweenAtack;
                    attack.damage = stats.damage;
                    attack.explosion = stats.explosion;
                    break;
                }
            case AttackType.Mousey:
                {
                    MouseyAttack attack = enemy.AddComponent<MouseyAttack>();
                    MouseyAttackStats stats = unlockedAttackType[RandAttack] as MouseyAttackStats;
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

        if (unlockedAttackType[RandAttack].type == AttackType.Baboom || unlockedAttackType[RandAttack].type == AttackType.Mousey)
        {
            NavRunToTarget nav = enemy.AddComponent<NavRunToTarget>();
        }
        else if (unlockedAttackType[RandAttack].type == AttackType.Doggo || unlockedAttackType[RandAttack].type == AttackType.Hound)
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
                case NavType.Walk:
                    {
                        NavRunToTarget nav = enemy.AddComponent<NavRunToTarget>();
                        break;
                    }
            }
        }
        else
        {
            int navType = Random.Range(0, unlockedNavType.Count);
            switch (unlockedNavType[navType].type)
            {
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
        switch (unlockedAttackType[RandAttack].type)
        {
            case AttackType.Baboom: //def = explode
                {
                    ExplodeOnDeath explosion = enemy.AddComponent<ExplodeOnDeath>();
                    explosion.explosion = explodeStats.explosion;
                    explosion.maximumDamage = explodeStats.damage;
                    explosion.radius = explodeStats.radius;
                    break;
                }
            case AttackType.Porcupine:  //Block
            case AttackType.Doggo: 
            case AttackType.Hound:
                {
                    if(!blockStats.unlocked || Random.value < 0.5f) break;
                    Block shield = enemy.AddComponent<Block>();
                    shield.blockHealTime = blockStats.blockHealTime;
                    shield.maximumBlockHealth = blockStats.maximumBlockHealth;
                    shield.healAmount = blockStats.healAmount;
                    break;
                }
            case AttackType.LightningHare:  //spawn decoy
            case AttackType.RockHare:
            case AttackType.SpearHare:
                {
                    if(!decoyStats.unlocked || Random.value < 0.5f) break;
                    Decoy decoy = enemy.AddComponent<Decoy>();
                    decoy.decoyObj = decoyStats.decoyObj;
                    break;
                }
        }
    }


    public GameObject GetEnemy()
    {
        GameObject enemy = Instantiate(baseEnemy);

        AssignAttack(enemy);
        AssignNav(enemy);
        AssignDef(enemy);

        return enemy;
    }
}

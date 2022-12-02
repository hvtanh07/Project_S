using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleSystem : MonoBehaviour
{
    //public GameObject baseEnemy;
    [SerializeField] private EnemyCreator enemyCreator;
    [SerializeField] private PowerUpManagement powerUpManagement;
    public static BattleSystem instance { get; private set; }
    public int numOfEnemyBeforeNextUpgrade;
    public int numOfKillForNextUpgradeIncrement;
    private int numOfEnemyOnMap;
    public int maxEnemyOnMap;
    public int killedEnemy;
    public bool AllowedToSpawn;



    private void Awake()
    {
        numOfEnemyBeforeNextUpgrade += numOfKillForNextUpgradeIncrement;
        enemyCreator = GetComponent<EnemyCreator>();
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        if (numOfEnemyOnMap >= maxEnemyOnMap)
        {
            AllowedToSpawn = false;
        }
        else
        {
            AllowedToSpawn = true;
        }

        if (killedEnemy >= numOfEnemyBeforeNextUpgrade)
        {
            //stop time
            //open update panel
            numOfEnemyBeforeNextUpgrade += numOfKillForNextUpgradeIncrement;
            Time.timeScale = 0f;
            powerUpManagement.gameObject.SetActive(true);
            
        }
    }

    public void enemyKilled()
    {
        killedEnemy++;
        numOfEnemyOnMap--;

    }

    public GameObject GetEnemySpawn()
    {
        numOfEnemyOnMap++;
        return enemyCreator.GetEnemy();
    }
}
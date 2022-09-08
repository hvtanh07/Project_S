using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleSystem : MonoBehaviour
{
    public GameObject baseEnemy;
    [SerializeField] private EnemyCreator enemyCreator;
    public static BattleSystem instance {get; private set;}
    public int numOfEnemyOnMap;
    public int maxEnemyOnMap;
    public int killedEnemy;
    public bool AllowedToSpawn;



    private void Awake() 
    { 
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
    private void Update() {
        if(numOfEnemyOnMap >= maxEnemyOnMap){
            AllowedToSpawn = false;
        }else{
            AllowedToSpawn = true;
        }
    }

    public void enemyKilled(){
        killedEnemy++;
        numOfEnemyOnMap--;
        
    }

    public GameObject GetEnemySpawn(){
        numOfEnemyOnMap++;
        return enemyCreator.GetEnemy(baseEnemy); 
    }
}
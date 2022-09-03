using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private List<EnemyToSpawn> enemies;
    public static BattleSystem instance {get; private set;}
    public int numOfEnemyOnMap;
    public int maxEnemyOnMap;
    public int killToNewEnemy;
    public int killedEnemy;
    public bool AllowedToSpawn;

    public int enemiesLockIndex;

    private void Awake() 
    { 
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
        if(killedEnemy >= killToNewEnemy){
            if (enemiesLockIndex + 1 < enemies.Count){
                enemiesLockIndex++;
            }
            //add new enemy to the list
        }
    }

    public GameObject GetEnemySpawn(){
        numOfEnemyOnMap++;
        if(enemiesLockIndex == 0){
            return enemies[0].getEnemy();
            //Instantiate(enemies[0], Spawnposition, Quaternion.identity);
        }
        else if (enemiesLockIndex > 0 && enemiesLockIndex < enemies.Count){  
            int i = UnityEngine.Random.Range(0, enemiesLockIndex + 1);
            return enemies[i].getEnemy();
            //Instantiate(enemies[UnityEngine.Random.Range(0, enemiesLockIndex)], Spawnposition, Quaternion.identity);
        }else{
            return null;
        }  
    }

    [System.Serializable]
    private class EnemyToSpawn{
        [SerializeField] private GameObject enemy;
        [SerializeField] public int Rarelity;

        public GameObject getEnemy(){
            return enemy;
        }
    }
}
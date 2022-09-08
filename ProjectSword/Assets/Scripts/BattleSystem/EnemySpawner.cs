using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float MinDistance;
    public float enemySpawnTime;
    [SerializeField] bool isPlayerInRange;
    float SpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Camera cam = Camera. main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Magnitude(target.position - transform.position) < MinDistance){
            isPlayerInRange = true;
        }else{
            isPlayerInRange = false;
        }
        
        if(!isPlayerInRange && Time.time - SpawnTime > enemySpawnTime && BattleSystem.instance.AllowedToSpawn){
            Instantiate(BattleSystem.instance.GetEnemySpawn(), transform.position, Quaternion.identity);
            SpawnTime = Time.time;
        }
    }
}



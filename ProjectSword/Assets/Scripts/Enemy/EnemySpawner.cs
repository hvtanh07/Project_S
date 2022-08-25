using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyToSpawn> enemies;
    [SerializeField] private Transform target;
    public float MinDistance;
    public int enemySpawnTime;
    [SerializeField] bool isPlayerInRange;
    float curentSpawnTime;

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
        curentSpawnTime += Time.time;
        if(!isPlayerInRange && curentSpawnTime > enemySpawnTime){
            //Spawn
            
            curentSpawnTime = Time.time;
        }

    }

    [System.Serializable]
    private class EnemyToSpawn{
        public Enemy enemiesSpawn;
        public bool StartSpawn;
    }
}



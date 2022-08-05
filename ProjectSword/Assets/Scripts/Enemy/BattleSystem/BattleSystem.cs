using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleSystem : MonoBehaviour
{
    public event EventHandler OnBattleStart;
    public event EventHandler OnBattleEnd;
    private enum State{
        Idle,
        Active,
        BattleOver,
    }
    [SerializeField] private Wave[] waves;
    [SerializeField] private ColliderTrigger colliderTrigger;
    private State state;
    private int currentWave;

    private void Awake() {
        state = State.Idle;
    }
    private void Start() {
        currentWave = 0;
        foreach (Wave wave in waves){
            wave.ApplyDeadEvent();
        }
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e){
        if(state == State.Idle){
            StartBattle();
            colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        }
        
    }
     
    private void StartBattle(){
        Debug.Log("Start Battle");
        state = State.Active;
        OnBattleStart?.Invoke(this, EventArgs.Empty);
        waves[currentWave].SpawnEnemies();
        //waves.SpawnEnemies();       
    }
    private void Update() {
        if(state == State.Active){
            if(waves[currentWave].numOfAlive <= 0){
                currentWave++;
                if (currentWave > waves.Length - 1){
                    state = State.BattleOver;
                    OnBattleEnd?.Invoke(this, EventArgs.Empty);
                    Debug.Log("Battle Over");
                }
                else{
                    waves[currentWave].SpawnEnemies();
                }
            }
        }
        
        

    }

    [System.Serializable]
    private class Wave{
        public int numOfAlive;
        [SerializeField] private Enemy[] enemyArray;
        public void SpawnEnemies(){
            foreach (Enemy enemy in enemyArray)
            {
                enemy.Spawn();
            } 
        }
        public void ApplyDeadEvent(){
            numOfAlive = enemyArray.Length;
            foreach (Enemy enemy in enemyArray)
            {
                enemy.OnEnemyDie += OnEnemyDie;
            }
        }

        private void OnEnemyDie(object sender, System.EventArgs e){
            numOfAlive--;
            //enemy.OnEnemyDie -= OnEnemyDie;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPack : SpawnerState
{
    public UnityEvent enemydie;
    public SpawnerState nextPack;
    public Enemy[] enemies;
    private int alive;
    private void Start() {
        for(int i =0; i< enemies.Length; i++){
            enemies[i].enabled = false;
        }
        alive = enemies.Length;
    }
    public override SpawnerState RunCurrentState(){
        for(int i =0; i< enemies.Length; i++){
            enemies[i].enabled = true;
            if(enemies[i].isDead()){
                
            }
        }
        if(alive <= 0){
            return nextPack;
        }
        else{
            return this;
        }
    }
}

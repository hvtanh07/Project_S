using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : SpawnerState
{
    public SpawnerState startingPack;
    private bool PlayerInArea;
    
    
    public override SpawnerState RunCurrentState(){
        if(PlayerInArea){
            return startingPack;
        }
        else{
            return this;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            PlayerInArea = true;
        }
    }
}

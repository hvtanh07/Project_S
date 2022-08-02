using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public SpawnerState currentState;
    private void Update() {
        RunSpawner();
    }

    private void RunSpawner(){
        SpawnerState nextState = currentState?.RunCurrentState();
        if(nextState != null){
            SwitchToNextPack(nextState);
        }
    }

    private void SwitchToNextPack(SpawnerState nextState){
        currentState = nextState;
    }
}


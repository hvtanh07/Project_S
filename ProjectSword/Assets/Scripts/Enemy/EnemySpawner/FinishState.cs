using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishState : SpawnerState
{  
    public override SpawnerState RunCurrentState(){
        //unlock for player to advance
        return this;    
    }
}

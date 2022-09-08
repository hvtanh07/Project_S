using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : State
{
    public bool reachAttackRange;
    public AttackState attackState;
    public override State RunCurrentState()
    {
        if(reachAttackRange){
            return attackState;
        }else{
            return this;
        }  
    }
}

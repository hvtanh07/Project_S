using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] private BattleSystem battleSystem;
    //reference door anim or door object

    private void Start() {
        //battleSystem.OnBattleStart += BattleSystem_BattleStarted;
        //battleSystem.OnBattleEnd += BattleSystem_BattleEnd;
    }

    private void BattleSystem_BattleStarted(object sender, System.EventArgs e){
        //Close entrance door
    }

    private void BattleSystem_BattleEnd(object sender, System.EventArgs e){
        //Open exit door
    }
}

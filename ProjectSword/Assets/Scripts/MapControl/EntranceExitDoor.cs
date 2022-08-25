using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceExitDoor : MonoBehaviour
{
    
    public BattleSystem battleSystem;
    public GameObject entranceDoor;
    public GameObject exitDoor;
    // Start is called before the first frame update
    void Start()
    {
        //battleSystem.OnBattleStart += OnBattleStart;
        //battleSystem.OnBattleEnd += OnBattleEnd;
    }
    

    private void OnBattleStart(object sender, System.EventArgs e){
        entranceDoor.SetActive(true);
        exitDoor.SetActive(true);
        
    }

    private void OnBattleEnd(object sender, System.EventArgs e){
        entranceDoor.SetActive(false);
        exitDoor.SetActive(false);
    }
}

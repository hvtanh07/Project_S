using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPoint : MonoBehaviour
{
    public bool unlocked;
    public bool activated;
    public Cards cardInfo;
    [SerializeField] CardFunc func;
    public List<UnlockPoint> nextUnlockPoint;
    private void Start() {
        func = GetComponent<CardFunc>();
    }
    public void ActivatePoint(){
        if(unlocked && !activated){
            activated = true;
            func.GiveAdditionalStats();
        }
        foreach(UnlockPoint point in nextUnlockPoint){
            point.unlocked = true;
        }
    }
}

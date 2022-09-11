using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCardList : MonoBehaviour
{
    [SerializeField] List<UnlockPoint> unlockPoints;
    UnlockPoint startingPoint;

    private void Start() {
        startingPoint = GetComponent<UnlockPoint>();
        GetlistOnnextUnlockedPoint();
    }
    
    void GetlistOnnextUnlockedPoint(){
        unlockPoints.Clear();
        checkPoint(startingPoint);
    }

    void checkPoint(UnlockPoint point){
        if(point.unlocked){
            if(!point.activated && !unlockPoints.Contains(point)){
                unlockPoints.Add(point);
            }else{
                foreach(UnlockPoint childpoint in point.nextUnlockPoint){
                    checkPoint(childpoint);
                }
            }
        }
    }

    public UnlockPoint GetRandomPoint(){
        int index = Random.Range(0,unlockPoints.Count-1);
        UnlockPoint result = unlockPoints[index];
        unlockPoints.Remove(unlockPoints[index]);
        return result;
    }
}

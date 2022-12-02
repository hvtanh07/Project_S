using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCardList : MonoBehaviour
{
    //Search through the tree for unlocked and !activated node to add to unlockPoints list
    [SerializeField] List<UnlockPoint> unlockPoints;
    UnlockPoint startingPoint;

    private void Start() {
        startingPoint = GetComponent<UnlockPoint>();
        GetlistOnnextUnlockedPoint();
    }
    
    public void GetlistOnnextUnlockedPoint(){
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

    //get a random node from list and delete it
    public UnlockPoint GetRandomPoint(){
        int index = Random.Range(0,unlockPoints.Count);
        UnlockPoint result = unlockPoints[index];
        unlockPoints.Remove(unlockPoints[index]);
        return result;
    }
}

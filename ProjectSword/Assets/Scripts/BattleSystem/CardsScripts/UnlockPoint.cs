using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPoint : MonoBehaviour
{
    public bool unlocked;
    public bool activated;
    public Cards cardInfo;
    [SerializeField] CardFunc[] funcs;
    public List<UnlockPoint> nextUnlockPoint;
    private void Start()
    {
        funcs = GetComponents<CardFunc>();
    }
    public void ActivatePoint()
    {
        if (unlocked && !activated)
        {
            activated = true;
            foreach (CardFunc func in funcs)
            {
                func.GiveAdditionalStats();
            }
        }
        foreach (UnlockPoint point in nextUnlockPoint)
        {
            point.unlocked = true;
        }
    }
}

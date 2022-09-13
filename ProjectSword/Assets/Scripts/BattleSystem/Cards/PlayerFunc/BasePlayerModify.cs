using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerModify : CardFunc
{
    public int additionalHealth;
    public float additionalWalkingSpeed;
    [SerializeField] Player stats;
    public override void GiveAdditionalStats()
    {
        stats.health += additionalHealth;
        stats.walkingSpeed += additionalWalkingSpeed;
    }
}

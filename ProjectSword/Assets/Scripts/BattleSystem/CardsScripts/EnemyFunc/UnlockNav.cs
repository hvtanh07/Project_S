using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockNav : CardFunc
{
    [SerializeField] NavStats stats;

    public override void GiveAdditionalStats()
    {
        if (!stats.unlocked)
        {
            stats.unlocked = true;
        }
    }
}

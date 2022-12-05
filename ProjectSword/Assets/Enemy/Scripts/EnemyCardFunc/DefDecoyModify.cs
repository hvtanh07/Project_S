using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefDecoyModify : CardFunc
{
    [SerializeField] DecoyStats stats;
    public GameObject decoyObjReplacement;

    public override void GiveAdditionalStats()
    {
        stats.decoyObj = decoyObjReplacement;
    }
}

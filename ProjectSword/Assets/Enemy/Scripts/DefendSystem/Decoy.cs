using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : Defend
{
    public GameObject decoyObj;
    bool decoyUsed;

    public override int Defending(int damage)
    {
        if (decoyUsed)
        {
            return damage;
        }
        else
        {
            Instantiate(decoyObj, transform.position, decoyObj.transform.rotation);
            //Play anim of disapearing
            //Calculate safe place to tele to
            transform.position += new Vector3(5,6,0);
            decoyUsed = true;
            return 0;
        }
    }
}

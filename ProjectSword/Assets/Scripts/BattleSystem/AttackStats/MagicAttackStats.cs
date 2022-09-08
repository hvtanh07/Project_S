using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackStats : AttackStats
{
    public GameObject lightning;
    public GameObject rock;
    public GameObject electric;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;   
    //public MagicType typeOfMagic;
    public LayerMask playerMask;
}

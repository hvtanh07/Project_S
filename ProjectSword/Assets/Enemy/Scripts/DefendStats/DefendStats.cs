using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefendType
{
    Block,
    Decoy,
    Explode
}
public class DefendStats : MonoBehaviour
{
    public DefendType type;
    public bool unlocked = false;
}
